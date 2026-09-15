using System.ComponentModel.DataAnnotations;
using API;
using Facet;
using Infra;
using LinqToDB;

[Facet(typeof(Book), exclude: nameof(Book.Author))]
public partial class BookDto;

[Facet(typeof(Author), exclude: nameof(Author.Books))]
public partial class AuthorDto
{
    public List<BookDto> Books { get; set; }
}

public class LibraryService(MyDatabaseConnection dbConnection)
{
    public List<AuthorDto> GetAuthors()
    {
        //Step 1: What data do we look up? (so here: joining authors and books
        var query = dbConnection.Authors.LoadWith(a => a.Books);
        
        //Step 2: Projection: how do transform the data before sending to client
        var list = query
            .Select(a => new AuthorDto(a)
            {
                Books = a.Books.Select(b => new BookDto(b)).ToList()
            })
            .ToList();
        return list;
    }
    
    public List<BookDto> GetBooks()
    {
        return dbConnection
            .Books
            .LoadWith(b => b.Author)
            .ThenLoad(a => a.Books)
           .Select(b => new BookDto(b))
            .ToList();
    }

    public void CreateBook(string title, string authorId)
    {
        dbConnection.Insert(new Book()
        {
            BookId = Guid.NewGuid().ToString(),
            BookTitle = title,
            AuthorId = authorId
        });
    }

    public void DeleteBook(string bookId)
    {
        var book = dbConnection.Books
                       .FirstOrDefault(b => b.BookId == bookId) ??
                   throw new ValidationException("Book not found!");
        dbConnection.Delete(book);
    }

    public void DeleteAuthor(string authorId)
    {
        using (var transaction = dbConnection.BeginTransaction())
        {
               var author = dbConnection.Authors
                                   .FirstOrDefault(b => b.AuthorId == authorId) ??
                               throw new ValidationException("Author not found!");
                    dbConnection.Books.Where(b => b.AuthorId == authorId).Delete();
                    dbConnection.Delete(author);
                    transaction.Commit();
        }
    }

    public void UpdateBook(UpdateBookRequestDto dto)
    {
        var book = dbConnection.Books
                       .FirstOrDefault(b => b.BookId == dto.BookIdForLookup) ??
                   throw new ValidationException("Book not found!");
        if(dto.NewBookTitle!=null)
            book.BookTitle = dto.NewBookTitle;

        dbConnection.Update(book);
    }
}
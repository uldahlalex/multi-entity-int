using System.ComponentModel.DataAnnotations;
using API;
using Infra;
using LinqToDB;

public class LibraryService(MyDatabaseConnection dbConnection)
{
    public List<Book> GetBooks()
    {
        return dbConnection.Books.LoadWith(b => b.Author).ToList();
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

using LinqToDB.Mapping;

namespace Infra;

public class Book
{
    [PrimaryKey] public string BookId { get; set; }
    [Column]public string BookTitle { get; set; }
    [Column]public string AuthorId { get; set; }
    [Association(ThisKey = nameof(AuthorId), OtherKey = nameof(Author.AuthorId))]
    public Author Author { get; set; }
}

public class Author
{

    [PrimaryKey]public string AuthorId { get; set; }
    [Column]public string AuthorName { get; set; }
    [Association(ThisKey = nameof(AuthorId), OtherKey = nameof(Book.AuthorId))]
    public List<Book> Books { get; set; }
    public int NumberOfBooksSold { get; set; }
}
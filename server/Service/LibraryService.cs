using Infra;
using LinqToDB;

public class LibraryService(MyDatabaseConnection dbConnection)
{
    public List<Book> GetBooks()
    {
        return dbConnection.Books.ToList();
    }

    public void CreateBook(string title)
    {
        dbConnection.Insert(new Book()
        {
            BookId = Guid.NewGuid().ToString(),
            BookTitle = title
        });
    }
}
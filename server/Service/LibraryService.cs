using Infra;

public class LibraryService(MyDatabaseConnection dbConnection)
{
    public List<Book> GetBooks()
    {
        return dbConnection.Books.ToList();
    }
}
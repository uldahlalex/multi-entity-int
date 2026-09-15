using Infra;
using LinqToDB;

public class Seeder(MyDatabaseConnection connectionToDb)
{
    public void Seed()
    {
        connectionToDb.CreateTable<Book>(tableOptions:TableOptions.CreateIfNotExists);
        connectionToDb.CreateTable<Author>(tableOptions:TableOptions.CreateIfNotExists);
        if (connectionToDb.Authors.Count() == 0)
        {
            connectionToDb.Insert(new Author
            {
                AuthorId = "1",
                AuthorName = "Bob"
            });
        }
    
        if (connectionToDb.Books.Count() == 0)
        {
            connectionToDb.Insert(new Book()
            {
                BookId = "1",
                BookTitle = "Bobs book",
                AuthorId = "1"
            });
        }
    }
}
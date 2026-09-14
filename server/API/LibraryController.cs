using Infra;
using Microsoft.AspNetCore.Mvc;

namespace API;


public class LibraryController(LibraryService service) : ControllerBase
{
    [HttpGet(nameof(GetBooks))]
    public List<Book> GetBooks()
    {
        return service.GetBooks();
    }

    [HttpPost(nameof(CreateBook))]
    public void CreateBook(string title)
    {
        service.CreateBook(title);
    }

    [HttpPut(nameof(UpdateBook))]
    public void UpdateBook()
    {
        throw new NotImplementedException();
    }

    [HttpDelete(nameof(DeleteBook))]
    public void DeleteBook(string bookId)
    {
        service.DeleteBook(bookId);
    }
}
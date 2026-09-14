using Microsoft.AspNetCore.Mvc;

namespace API;


public class LibraryController : ControllerBase
{
    [HttpGet(nameof(GetBooks))]
    public List<object> GetBooks()
    {
        throw new NotImplementedException();
    }

    [HttpPost(nameof(CreateBook))]
    public void CreateBook()
    {
        throw new NotImplementedException();
    }

    [HttpPut(nameof(UpdateBook))]
    public void UpdateBook()
    {
        throw new NotImplementedException();
    }

    [HttpDelete(nameof(DeleteBook))]
    public void DeleteBook()
    {
        throw new NotImplementedException();
    }
}
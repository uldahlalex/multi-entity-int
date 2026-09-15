using Infra;
using Microsoft.AspNetCore.Mvc;

namespace API;


/// <summary>
/// For storing user in database
/// </summary>
public class User
{
    public string PasswordHash { get; set; }
    public string Username { get; set; }
}

/// <summary>
/// For sending over network
/// </summary>
public class UserDto
{
    public string Username { get; set; }
}

public class LibraryController(LibraryService service) : ControllerBase
{

 
    [HttpGet(nameof(GetBooks))]
    public List<AuthorDto> GetBooks()
    {
        return service.GetAuthors();
    }

    [HttpPost(nameof(CreateBook))]
    public void CreateBook(string title, string authorId)
    {
        service.CreateBook(title, authorId);
    }

    [HttpPut(nameof(UpdateBook))]
    public void UpdateBook(UpdateBookRequestDto dto)
    {
        service.UpdateBook(dto);
    }

    [HttpDelete(nameof(DeleteBook))]
    public void DeleteBook(string bookId)
    {
        service.DeleteBook(bookId);
    }
}
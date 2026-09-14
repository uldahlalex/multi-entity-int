namespace API;

public class UpdateBookRequestDto
{
    public string BookIdForLookup { get; set; }
    public string? NewBookTitle { get; set; }
}

using LinqToDB.Mapping;

namespace Infra;

public class Book
{
    [PrimaryKey] public string BookId { get; set; }
    [Column]public string BookTitle { get; set; }
}
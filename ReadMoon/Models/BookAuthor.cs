namespace ReadMoon.Models;

public class BookAuthor
{
    public Book Book { get; set; }
    public int BookId { get; set; }
    
    public Author Author { get; set; }
    public int AuthorId { get; set; }
}
namespace ReadMoon.Models;

public class Author
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Bio { get; set; }
    public string ImageURL { get; set; }
    
    //Relationships
    public List<Book> Books { get; set; }

}
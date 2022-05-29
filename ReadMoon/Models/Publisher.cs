namespace ReadMoon.Models;

public class Publisher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageURL { get; set; }
    
    //Relationships
    public List<Book> Books { get; set; } = new List<Book>();

}
using ReadMoon.Data.Base;

namespace ReadMoon.Models;

public class Book : IEntityBase
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime RelaseDate { get; set; }
    public string ISBN { get; set; }
    public string ImageURL { get; set; }
    public string Description { get; set; }
    
    //Relationships
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    
    public Publisher Publisher { get; set; }
    public int PublisherId { get; set; }
    
    public List<Author> Author { get; set; }
}
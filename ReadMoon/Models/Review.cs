using ReadMoon.Data.Base;

namespace ReadMoon.Models;

public class Review : IEntityBase
{
    public int Id { get; set; }
    public string Text  { get; set; }
    public DateTime CreatedOn { get; set; }
    
    //Relationships
    public Book Books { get; set; }
    public int BookId { get; set; }
    public User Users { get; set; }
    public string UserId { get; set; }
}
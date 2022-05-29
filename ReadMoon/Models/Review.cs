namespace ReadMoon.Models;

public class Review
{
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    
    //Relationships
    public User User { get; set; }
    public Guid UserId { get; set; }
}
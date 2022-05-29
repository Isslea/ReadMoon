namespace ReadMoon.Models;

public class User
{
    public Guid Id { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    
    //Relationships
    public Review Review { get; set; }
}
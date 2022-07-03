using Microsoft.AspNetCore.Identity;

namespace ReadMoon.Models;

public class User : IdentityUser
{
    public virtual IEnumerable<Review>? Reviews { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ReadMoon.Data;

public class LoginVM
{
    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Adres email jest wymagany")]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Hasło jest wymagane")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
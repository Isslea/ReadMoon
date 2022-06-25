using System.ComponentModel.DataAnnotations;

namespace ReadMoon.Data;

public class RegisterVM
{
    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "E-mail jest wymagany")]
    public string EmailAddress { get; set; }

    [Display(Name = "Nazwa użytkownika")]
    [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
    public string UserName { get; set; }
    
    [Display(Name = "Hasło")]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Potwierdź hasło")]
    [Required(ErrorMessage = "Wymagane jest potwierdzić hasło")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
    public string ConfirmPassword { get; set; }
}
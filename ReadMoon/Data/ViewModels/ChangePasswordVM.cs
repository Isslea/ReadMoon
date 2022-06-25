using System.ComponentModel.DataAnnotations;

namespace ReadMoon.Data;

public class ChangePasswordVM
{
    [Required(ErrorMessage = "Hasło jest wymagane")]
    [DataType(DataType.Password)]
    [Display(Name = "Obecne hasło")]
    public string OldPassword { get; set; }
    
    [Required(ErrorMessage = "Hasło jest wymagane")]
    [DataType(DataType.Password)]
    [Display(Name = "Nowe hasło")]
    public string NewPassword { get; set; }
    
    [Required(ErrorMessage = "Hasło jest wymagane")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Hasła nie są takie same")]
    [Display(Name = "Potwierdź hasło")]
    public string ConfirmNewPassword { get; set; }
    
}
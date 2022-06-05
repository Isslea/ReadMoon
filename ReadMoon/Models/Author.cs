using System.ComponentModel.DataAnnotations;
using ReadMoon.Data.Base;

namespace ReadMoon.Models;

public class Author : IEntityBase
{
    public int Id { get; set; }
    [Display(Name = "Autor")]
    [Required(ErrorMessage = "Author jest wymagany")]
    public string FullName { get; set; }
    [Display(Name = "Biografia")]
    [Required(ErrorMessage = "Biografia jest wymagana")]
    public string Bio { get; set; }

    [Display(Name = "Zdjęcie")]
    public string ImageURL { get; set; }
    
    //Relationships
    public List<Book> Books { get; set; }

}
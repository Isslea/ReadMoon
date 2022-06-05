using System.ComponentModel.DataAnnotations;
using ReadMoon.Data.Base;

namespace ReadMoon.Models;

public class Publisher : IEntityBase
{
    public int Id { get; set; }
    
    [Display(Name = "Wydawnictwo")]
    [Required(ErrorMessage = "Nazwa wydawnictwa jest wymagana")]
    public string Name { get; set; }

    [Display(Name = "Opis")]
    [Required(ErrorMessage = "Opis jest wymagany")]
    public string Description { get; set; }
        
    [Display(Name = "Zdjęcie")]
    public string ImageURL { get; set; }
    
    //Relationships
    public List<Book> Books { get; set; } = new List<Book>();

}
using System.ComponentModel.DataAnnotations;
using ReadMoon.Data.Base;

namespace ReadMoon.Models;

public class Category : IEntityBase
{
    public int Id { get; set; }
    [Display(Name = "Nazwa")]
    [Required(ErrorMessage = "Nazwa kategorii jest wymagana!")]
    public string Name { get; set; }
    
    //Relationships
    public List<Book> Books { get; set; } = new List<Book>();

}
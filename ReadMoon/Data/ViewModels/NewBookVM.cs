using System.ComponentModel.DataAnnotations;
using ReadMoon.Models;

namespace ReadMoon.Data;

public class NewBookVM
{
    public int Id { get; set; }
    [Display(Name = "Tytuł książki")]
    [Required(ErrorMessage = "Tytuł jest wymagany!")]
    public string Title { get; set; }
       
    [Display(Name = "Data wydania książki")]
    [Required(ErrorMessage = "Data wydania jest wymagana!")]
    public DateTime RelaseDate { get; set; }
    
    public string ISBN { get; set; }
   
    [Display(Name = "Zdjęcie książki")]
    public string ImageURL { get; set; }
   
    [Display(Name = "Opis książki")]
    [Required(ErrorMessage = "Opis jest wymagany!")]
    public string Description { get; set; }
    
    //Relationships
    [Display(Name = "Wybierz Kategorie")]
    [Required(ErrorMessage = "Kategoria jest wymagana!")]
    public int CategoryId { get; set; }
    
    [Display(Name = "Wybierz Wydawcę")]
    [Required(ErrorMessage = "Wydawca jest wymagany!")]
    public int PublisherId { get; set; }
    
    [Display(Name = "Wybierz Autora")]
    [Required(ErrorMessage = "Autor jest wymagany!")]
    public List<int> AuthorId { get; set; }
}
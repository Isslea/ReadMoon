using System.ComponentModel.DataAnnotations;

namespace ReadMoon.Data;

public class ReviewVM
{
    public int Id { get; set; }
    [Display(Name = "Treść komenentarza")]
    [Required(ErrorMessage = "Treść komenatarza jest wymagana!")]
    public string Text  { get; set; }
    public DateTime CreatedOn { get; set; }

    //Relationships
    public int BookId { get; set; }
    public string UserId { get; set; }
    
    
    
}
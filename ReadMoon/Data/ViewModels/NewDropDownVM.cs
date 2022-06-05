using ReadMoon.Models;

namespace ReadMoon.Data;

public class NewDropDownVM
{
    public NewDropDownVM()
    {
        Authors = new List<Author>();
        Publishers = new List<Publisher>();
        Categories = new List<Category>();
    }

    public List<Author> Authors { get; set; }
    public List<Publisher> Publishers { get; set; }
    public List<Category> Categories { get; set; }
}
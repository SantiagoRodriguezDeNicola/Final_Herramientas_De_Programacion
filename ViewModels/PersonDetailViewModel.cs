using System.ComponentModel.DataAnnotations;
using Final.Models;
using Final.Utils;

namespace Final.ViewModels;

public class PersonDetailViewModel{
    public int Id { get; set; }
    [Display(Name="Nombre")]
    public string Name { get; set; }
        [Display(Name="Apellido")]
    public string LastName { get; set; }
    [Display(Name="Año de nacimiento")]
    public int Birthdate { get; set; }
    [Display(Name="Talle")]
    public Weist Weist { get; set; }
    public virtual List<Stock>? Stocks { get; set; } 
}

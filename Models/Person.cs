using System.ComponentModel.DataAnnotations;
using Final.Utils;

namespace Final.Models;

public class Person {
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
using System.ComponentModel.DataAnnotations;

namespace Final.Models;

public class Brand {
    public int Id { get; set; }
    [Display(Name="Nombre de la marca")]
    public string Name { get; set; }

    [Display(Name="Email de la marca")]
    public string Email{get; set;}

    public virtual List<Clothe>? Clothes {get; set;} 

}
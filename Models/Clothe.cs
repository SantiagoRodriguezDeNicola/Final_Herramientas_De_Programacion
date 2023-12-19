using System.ComponentModel.DataAnnotations;
using Final.Utils;

namespace Final.Models;

public class Clothe {
    public int Id { get; set; }

    [Display(Name="Nombre de la prenda")]
    public string Name { get; set; }
    [Display(Name="Descripcion de la prenda")]
    public string Description { get; set; }
    [Display(Name="Talle de la prenda")]
    public Weist Weist { get; set; }
    [Display(Name="Precio")]
    public int Price { get; set; }
    public virtual List<Stock>? Stocks { get; set; } 
    public virtual List<Brand>? Brands { get; set; }
}
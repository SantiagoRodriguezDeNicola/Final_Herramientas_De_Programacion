using System.ComponentModel.DataAnnotations;
namespace Final.Models;

public class Stock {
    public int Id { get; set; }
    public int? ClotheId { get; set; }
    public int? BrandId { get; set; }
    public virtual Clothe? Clothe { get; set; } 
    public virtual Brand? Brand { get; set; } 
    [Display(Name="Cantidad")]
    public int Quantity { get; set; }
    public virtual List<Stock>? Stocks { get; set; } 
}
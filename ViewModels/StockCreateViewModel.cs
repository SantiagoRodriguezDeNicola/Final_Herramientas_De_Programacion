using Final.Models;

namespace Final.ViewModels;

public class StockCreateViewModel{

    public int PersonId { get; set;}
    public int BrandId { get; set;}

    public List<Clothe>? Clothes { get; set;}

    public Stock Stock { get; set;} = new();
}
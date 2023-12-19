using Final.Models;

namespace Final.ViewModels;

public class BrandSearchViewModel
{
    public List<Brand> Brands {get; set;} = new List<Brand>();

    public string? NameFilter { get; set; }
}
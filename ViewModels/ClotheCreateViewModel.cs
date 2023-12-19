using System.ComponentModel.DataAnnotations;
using Final.Utils;
using Final.Models;

namespace Final.ViewModels;

public class ClotheCreateViewModel
{
    public Clothe Clothe { get; set; }
    public List<int>? SelectedBrands {get; set;} = new List<int>();
    public List<Brand>? Brands {get; set;} 
}
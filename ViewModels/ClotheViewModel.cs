using Final.Models;

namespace Final.ViewModels;

public class ClotheViewModel {
    public List<Clothe> Clothes { get; set; } = new List<Clothe>();
    public string? NameFilter { get; set; }
    public Clothe Clothe { get; set; } = new Clothe();
}
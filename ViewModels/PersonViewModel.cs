using Final.Models;

namespace Final.ViewModels;

public class PersonViewModel {
    public List<Clothe> Clothes { get; set; } = new List<Clothe>();
    public List<Person>? Persons { get; set; } = new List<Person>();
    public string? NameFilter { get; set; }
    public virtual List<Stock>? Stocks { get; set; }
    public Person Person { get; set; } = new();
    public int quantityInStock { get; set; }
    public int Id { get; set; }
    public int Quantity{ get; set; }
    public double total { get; set; }
}
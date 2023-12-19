using Final.Data;
using Final.Models;
using Final.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Final.Services;

public class PersonService : IPersonService
{
    private readonly ClotheContext _context;
    public PersonService(ClotheContext clotheContext){
        _context = clotheContext;
    }
    public void Delete(Person obj)
    {
        if(obj.Stocks != null){
            _context.Stock.RemoveRange(obj.Stocks);
        }        
        _context.Person.Remove(obj);
    }

    public PersonViewModel GetAll()
    {
        var query = GetQuery();
        PersonViewModel persons = new PersonViewModel();
        persons.Persons = query.ToList();

        return persons;
    }

    public PersonViewModel GetAll(string nameFilter)
    {
        var query = GetQuery();
        query = query.Where(x => x.Name.ToLower().Contains(nameFilter.ToLower()) || x.Name.ToString().Contains(nameFilter));

        PersonViewModel persons = new PersonViewModel();
        persons.Persons = query.ToList();

        return persons;
    }

    public PersonViewModel? GetPersonWithStockById(int id)
    {
        int quantityInStock = 0;
        double total = 0;
        PersonViewModel personViewModel = new PersonViewModel();
        var person = _context.Person.Include(x=> x.Stocks).ThenInclude(i => i.Clothe)
        .FirstOrDefault(m => m.Id == id);

        personViewModel.Person = person;
        if(person.Stocks != null){
            foreach(Stock i in person.Stocks){
                quantityInStock += i.Quantity;
                total += i.Clothe.Price * i.Quantity;
            }
        }
        personViewModel.quantityInStock = quantityInStock;
        personViewModel.total = total;

        return personViewModel;
    }

    public Person? GetById(int id)
    {
        var person = _context.Person.FirstOrDefault(m => m.Id == id);
        return person;
    }

    public void Update(Person obj)
    {
        _context.Update(obj);
        _context.SaveChanges();
    }

    private IQueryable<Person> GetQuery()
    {
        return from Person in _context.Person select Person;
    }
}
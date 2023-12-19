using Final.Models;
using Final.ViewModels;

namespace Final.Services;

public interface IPersonService
{
    PersonViewModel GetAll();
    PersonViewModel GetAll(string nameFilter);
    void Delete(Person obj);
    Person? GetById(int id);
    PersonViewModel? GetPersonWithStockById(int id);
    void Update(Person person);
}
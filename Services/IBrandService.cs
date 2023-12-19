using Final.ViewModels;
using Final.Models;
namespace Final.Services;

public interface IBrandService
{
    BrandSearchViewModel GetAll();
    void Update(Brand obj);
    void Delete(Brand obj);
    Brand? GetById(int id);
    BrandSearchViewModel GetAll(string nameFilter);
}
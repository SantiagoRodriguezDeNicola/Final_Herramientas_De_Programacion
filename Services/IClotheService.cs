using Final.Models;
using Final.ViewModels;

namespace Final.Services;

public interface IClotheService
{
    ClotheViewModel GetAll();
    void Update(ClotheCreateViewModel obj);
    void Delete(Clothe obj);
    Clothe? GetById(int id);
    ClotheViewModel GetAll(string nameFilter);
}
using Final.Data;
using Final.Models;
using Final.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Final.Services;

public class ClotheService : IClotheService
{
    private readonly ClotheContext _context;
    public ClotheService(ClotheContext context){
        _context = context;
    }
    public void Delete(Clothe obj)
    {
        _context.Remove(obj);
        _context.SaveChangesAsync();
    }

    public ClotheViewModel GetAll()
    {
        var query = GetQuery();
        ClotheViewModel clothes = new ClotheViewModel();
        clothes.Clothes = query.ToList();
        return clothes;
    }

    public ClotheViewModel GetAll(string nameFilter)
    {
        var query = GetQuery();
        query = query.Where(x => x.Name.ToLower().Contains(nameFilter.ToLower()) || x.Name.ToString().Contains(nameFilter));
        
        ClotheViewModel clothes = new ClotheViewModel();
        clothes.Clothes = query.ToList();
        return clothes;
    }

    public Clothe? GetById(int id)
    {
        var clothe = _context.Clothe.Include(x => x.Brands).FirstOrDefault(m => m.Id == id);
        return clothe;   
    }

    public void Update(ClotheCreateViewModel model)
    {
        List<Brand> brands;
        var clothe = model.Clothe;
        if(model.SelectedBrands != null && model.SelectedBrands.Count() > 0){
            brands = _context.Brand.Where(a => model.SelectedBrands.Contains(a.Id)).ToList();
        }     
        _context.Update(clothe);
        _context.SaveChanges();
    }
    private IQueryable<Clothe> GetQuery()
    {
        return from clothe in _context.Clothe select clothe;
    }
}
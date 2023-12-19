using Final.Data;
using Final.Models;
using Final.ViewModels;

namespace Final.Services;

public class BrandService : IBrandService
{
    private readonly ClotheContext _context;

    public BrandService(ClotheContext clotheContext){
        _context = clotheContext;
    }

    public void Delete(Brand obj)
    {
        _context.Remove(obj);
        _context.SaveChangesAsync();
    }

    public BrandSearchViewModel GetAll()
    {
        var query = GetQuery();
        BrandSearchViewModel brandViewModel = new BrandSearchViewModel();
        brandViewModel.Brands = query.ToList();
        return brandViewModel;
    }

    public BrandSearchViewModel GetAll(string nameFilter)
    {
        var query = GetQuery();
        query = query.Where(x => x.Name.ToLower().Contains(nameFilter.ToLower()) || x.Name.ToLower().Contains(nameFilter.ToLower()));
        
        BrandSearchViewModel brandViewModel = new BrandSearchViewModel();
        brandViewModel.Brands = query.ToList();
        return brandViewModel;
    }

    public Brand? GetById(int id)
    {
        var brand = _context.Brand
        .FirstOrDefault(m => m.Id == id);
        return brand;
    }

    public void Update(Brand obj)
    {
        _context.Update(obj);
        _context.SaveChanges();
    }

    private IQueryable<Brand> GetQuery()
    {
        return from brand in _context.Brand select brand;
    }
}
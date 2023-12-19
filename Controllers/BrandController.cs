using Final.Models;
using Final.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Final.ViewModels;

namespace Final.Controllers;
[Authorize]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService){
        _brandService = brandService;
    }

    public IActionResult Index(string? nameFilter)
    {
        BrandSearchViewModel brandSearchViewModel;
        if(!string.IsNullOrEmpty(nameFilter)){
            brandSearchViewModel = _brandService.GetAll(nameFilter);
        }else{
            brandSearchViewModel = _brandService.GetAll();
        }
        return View(brandSearchViewModel);
    }

    [Authorize(Roles = "admin, empleado")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([Bind("Id,Name,Email")]Brand model)
    {
        if (ModelState.IsValid)
        {
            _brandService.Update(model);
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "admin, empleado")]
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var song = _brandService.GetById(id.Value);
        if (song == null)
        {
            return NotFound();
        }

        return View(song);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var brand = _brandService.GetById(id);
        if (brand != null)
        {
            _brandService.Delete(brand);
        }
                    
        return RedirectToAction(nameof(Index));
    }
}
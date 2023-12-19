using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Models;
using Final.ViewModels;
using Final.Services;
using Microsoft.AspNetCore.Authorization;

namespace Final.Controllers
{
    [Authorize]
    public class ClotheController : Controller
    {
        private readonly IClotheService _clotheService;
        private readonly IBrandService _brandService;

        public ClotheController(IClotheService clotheService, IBrandService brandService)
        {
            _clotheService = clotheService;
            _brandService = brandService;
        }

        [Authorize(Roles = "admin, empleado")]
        public IActionResult Index(string? nameFilter)
        {
            ClotheViewModel clothes;

            if(!string.IsNullOrEmpty(nameFilter)){
                clothes = _clotheService.GetAll(nameFilter);
            }else{
                clothes = _clotheService.GetAll();
            }

            return clothes != null ? 
            View(clothes) :
            Problem("Entity set 'ClotheContext.Clothe'  is null.");
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothe = _clotheService.GetById(id.Value);

            if (clothe == null)
            {
                return NotFound();
            }

            return View(clothe);
        }

        public IActionResult Create()
        {
            ClotheCreateViewModel model = new ClotheCreateViewModel();
            var brands = _brandService.GetAll();
            model.Brands = brands.Brands;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClotheCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _clotheService.Update(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model.Clothe);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothe =  _clotheService.GetById(id.Value);
            if (clothe == null)
            {
                return NotFound();
            }
            return View(clothe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ClotheCreateViewModel model)
        {
            if (id != model.Clothe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clotheService.Update(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(model.Clothe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model.Clothe);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothe = _clotheService.GetById(id.Value);

            if (clothe == null)
            {
                return NotFound();
            }

            return View(clothe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var clothe = _clotheService.GetById(id);
            if (clothe != null)
            {
                _clotheService.Delete(clothe);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
          return _clotheService.GetById(id) != null;
        }
    }
}

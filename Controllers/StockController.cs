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
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IClotheService _clotheService;

        public StockController(IStockService stockService, IClotheService clotheService)
        {
            _stockService = stockService;
            _clotheService = clotheService;
        }

        public IActionResult Index()
        {
            var stocks = _stockService.GetAll();
            return stocks != null ? 
            View(stocks) :
            Problem("Entity set 'AlbumContext.Stock'  is null.");
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = _stockService.GetById(id.Value);

            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        [Authorize(Roles = "admin, empleado")]

        public IActionResult Create(int id)
        {
            StockCreateViewModel stockCreate = new StockCreateViewModel();
            stockCreate.BrandId = id;
            stockCreate.Clothes = _clotheService.GetAll().Clothes;
            stockCreate.Stock = new Stock();

            return View(stockCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,AlbumId,ArtistId,Quantity")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                var stockAlbum = _stockService.GetStockByArtistIdAndAlbumId(stock.BrandId.Value, stock.ClotheId.Value);
                if(stockAlbum == null){
                    _stockService.Update(stock);                    
                } else{
                    int quantity = stockAlbum.Quantity + stock.Quantity;
                    stockAlbum.Quantity = quantity;
                    _stockService.Update(stockAlbum);
                }

                return RedirectToAction("Stock", "Artist", new { id = stock.BrandId });
            }
            return View(stock);
        }

        [Authorize(Roles = "admin, empleado")]

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = _stockService.GetById(id.Value);

            if (stock == null)
            {
                return NotFound();
            }
            return View(stock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CLotheId,BrandId,Quantity")] Stock stock)
        {
            if (id != stock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _stockService.Update(stock);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Stock", "Artist", new { id = stock.BrandId });
            }
            return View(stock);
        }

        [Authorize(Roles = "admin, empleado")]

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = _stockService.GetById(id.Value);

            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var stock = _stockService.GetById(id);
            if (stock != null)
            {
                _stockService.Delete(stock);
            }

            return RedirectToAction("Stock", "Artist", new { id = stock.BrandId });
        }

        private bool StockExists(int id)
        {
          return _stockService.GetById(id) != null;
        }
    }
}

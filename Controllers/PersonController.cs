using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Models;
using Final.Utils;
using Final.ViewModels;
using Final.Services;
using Microsoft.AspNetCore.Authorization;

namespace Final.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: Artist
        public IActionResult Index(string nameFilter)
        {
            PersonViewModel persons;
            
            if (!string.IsNullOrEmpty(nameFilter))
            {
                persons = _personService.GetAll(nameFilter);
            }else{
                persons = _personService.GetAll();
            }

            return persons != null ? 
            View(persons) :
            Problem("Entity set 'AlbumContext.Artist'  is null.");
        }

        public IActionResult Stock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _personService.GetPersonWithStockById(id.Value);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Artist/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _personService.GetById(id.Value);

            if (person == null)
            {
                return NotFound();
            }

            var viewModel = new PersonDetailViewModel();
            viewModel.Name = person.Name;
            viewModel.LastName = person.LastName;
            viewModel.Birthdate = person.Birthdate;
            viewModel.Weist = person.Weist;

            return View(viewModel);
        }

        [Authorize(Roles = "admin, empleado")]
        // GET: Artist/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,LastName,Birthdate,Weist")] Person person)
        {
            if (ModelState.IsValid)
            {
                _personService.Update(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        [Authorize(Roles = "admin, empleado")]
        // GET: Artist/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _personService.GetById(id.Value);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Artist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,LastName,Birthdate,Weist")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _personService.Update(person);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(person.Id))
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
            return View(person);
        }

        [Authorize(Roles = "admin, empleado")]
        // GET: Artist/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _personService.GetById(id.Value);
            
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }


        /*
        Este es un método que maneja la eliminación de un artista junto con su stock asociado. 
        Primero, se verifica si la tabla de Artist existe. Luego, se utiliza el método Include para 
        cargar los datos de la tabla Stock y Album relacionados con el artista que se va a eliminar. 
        Luego, se utiliza el método RemoveRange para eliminar todo el stock del artista y finalmente 
        se elimina al artista de la tabla Artist.
        */
        // POST: Artist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var personViewModel = _personService.GetPersonWithStockById(id);
            if (personViewModel.Person != null)
            {
                _personService.Delete(personViewModel.Person);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _personService.GetById(id) != null;
        }
    }
}

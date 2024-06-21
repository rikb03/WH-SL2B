using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dierentuin.Data;
using Dierentuin.Models;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Dierentuin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DierentuinContext _context;

        public CategoriesController(DierentuinContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(string search)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'DierentuinContext.Category' is null.");
            }

            var category = _context.Category.Select(c => c);

            if (!String.IsNullOrEmpty(search))
            {
                category = category.Where(c => c.Name!.Contains(search));
            }

            return View(await category.Include(c => c.Animals).ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .Include(c => c.Animals)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            List<Animal> animals = await _context.Animal.Include(a => a.Enclosure).ToListAsync();
            Collection<SelectListItem> animalSelectList = new Collection<SelectListItem>();
            //List<string> animalSelected = new List<string>();

            foreach (Animal animal in animals)
            {
                SelectListGroup group = null;
                if (animal.Enclosure != null) { group = new SelectListGroup { Name = animal.Enclosure.Name }; } // If the animal has an enclosure, add it to the enclosure group
                //if (animal.CategoryId == id) { animalSelected.Add(animal.Id.ToString()); } // If the animal is in the category, add it to the selected list
                //bool animalSelected = animal.CategoryId == id; // If the animal is in the category, select it
                bool animalSelected = category.Animals.Any(a => a.Id == animal.Id);
                animalSelectList.Add(new SelectListItem() { Value = animal.Id.ToString(), Text = animal.Name, Group = group, Selected = animalSelected }); // Add the animal to the list
            }

            //ViewBag.animalSelected = animalSelected ?? new List<string>();  // Ensure it's not null
            ViewBag.animalsList = animalSelectList;

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Animals")] Category category, List<string> Animals)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category = await _context.Category.Include("Animals").FirstOrDefaultAsync(c => c.Id == id);

                    foreach(Animal animal in category.Animals)
                    {
                        animal.CategoryId = null; // Removes the category from all the animals currently in the category
                    }

                    foreach(string animalId in Animals)
                    {
                        int animalIdInt = Int32.Parse(animalId);
                        Animal animal = await _context.Animal.FindAsync(animalIdInt);

                        animal.CategoryId = id; // Puts all the selected animals in the category
                    }

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .Include(c => c.Animals)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                _context.Category.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}

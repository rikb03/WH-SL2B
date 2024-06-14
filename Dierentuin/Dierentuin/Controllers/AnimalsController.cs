using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dierentuin.Data;
using Dierentuin.Models;

namespace Dierentuin.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly DierentuinContext _context;

        public AnimalsController(DierentuinContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index(string searchString, string species, Animal.SizeType? size, Animal.DietaryClassType? dietary, Animal.ActivityPatternType? activityPattern, int? prey, Enclosure.SecurityLevelType? securityRequirement)
        {
            var animals = from a in _context.Animal.Include(a => a.Category).Include(a => a.Enclosure)
                          select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                animals = animals.Where(a => a.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(species))
            {
                animals = animals.Where(a => a.Species.Contains(species));
            }

            if (size.HasValue)
            {
                animals = animals.Where(a => a.Size == size.Value);
            }

            if (dietary.HasValue)
            {
                animals = animals.Where(a => a.Dietary == dietary.Value);
            }

            if (activityPattern.HasValue)
            {
                animals = animals.Where(a => a.ActivityPattern == activityPattern.Value);
            }

            if (prey != null)
            {
                animals = animals.Where(a => a.Prey == prey);
            }

            if (securityRequirement.HasValue)
            {
                animals = animals.Where(a => a.SecurityRequirement == securityRequirement.Value);
            }

            return View(await animals.ToListAsync());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.Category)
                .Include(a => a.Enclosure)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            Animal animal = new Animal();
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            ViewData["EnclosureId"] = new SelectList(_context.Set<Enclosure>(), "Id", "Name");

            return View(animal);
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Species,CategoryId,Size,Dietary,ActivityPattern,Prey,EnclosureId,SpaceRequirement,SecurityRequirement,Image")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", animal.CategoryId);
            ViewData["EnclosureId"] = new SelectList(_context.Set<Enclosure>(), "Id", "Name", animal.EnclosureId);
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", animal.CategoryId);
            ViewData["EnclosureId"] = new SelectList(_context.Set<Enclosure>(), "Id", "Name", animal.EnclosureId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Species,CategoryId,Size,Dietary,ActivityPattern,Prey,EnclosureId,SpaceRequirement,SecurityRequirement")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", animal.CategoryId);
            ViewData["EnclosureId"] = new SelectList(_context.Set<Enclosure>(), "Id", "Name", animal.EnclosureId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.Category)
                .Include(a => a.Enclosure)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {
                _context.Animal.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.Id == id);
        }

        // GET: Animals/Sunrise/5
        public async Task<IActionResult> Sunrise(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            ViewBag.Message = animal.Sunrise();
            return View("Details", animal);
        }

        // GET: Animals/Sunset/5
        public async Task<IActionResult> Sunset(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            ViewBag.Message = animal.Sunset();
            return View("Details", animal);
        }

        // GET: Animals/FeedingTime/5
        public async Task<IActionResult> FeedingTime(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Animal animal = await _context.Animal.Include(a => a.Enclosure).FirstOrDefaultAsync(a => a.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            Category prey = await _context.Category.FirstOrDefaultAsync(c => c.Id == animal.Prey);
            ViewBag.Message = animal.FeedingTime(animal.Enclosure, prey);
            return View("Details", animal);
        }

        // GET: Animals/CheckConstraint/5
        public async Task<IActionResult> CheckConstraint(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.Include(a => a.Enclosure).FirstOrDefaultAsync(a => a.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            ViewBag.Message = animal.CheckConstraint(animal.Enclosure);
            return View("Details", animal);
        }
    }
}

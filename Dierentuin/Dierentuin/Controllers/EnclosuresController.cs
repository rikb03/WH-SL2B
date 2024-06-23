using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dierentuin.Data;
using Dierentuin.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;

namespace Dierentuin.Controllers
{
    public class EnclosuresController : Controller
    {
        private readonly DierentuinContext _context;

        public EnclosuresController(DierentuinContext context)
        {
            _context = context;
        }

        // GET: Enclosures
        public async Task<IActionResult> Index(string search)
        {
            if (_context.Enclosure == null)
            {
                return Problem("Entity set 'DierentuinContext.Enclosure' is null.");
            }

            var enclosure = _context.Enclosure.Select(c => c);

            if (!String.IsNullOrEmpty(search))
            {
                enclosure = enclosure.Where(c => c.Name!.Contains(search));
            }

            return View(await enclosure.Include(c => c.Animals).ToListAsync());
        }

        // GET: Enclosures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enclosure = await _context.Enclosure
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enclosure == null)
            {
                return NotFound();
            }

            ViewBag.animals = await _context.Animal.Where(a => a.EnclosureId == id).ToListAsync();

            return View(enclosure);
        }

        // GET: Enclosures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enclosures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Climate,Habitat,SecurityLevel,Size")] Enclosure enclosure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enclosure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enclosure);
        }

        // GET: Enclosures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enclosure = await _context.Enclosure.Include(a => a.Animals).FirstOrDefaultAsync(m => m.Id == id);
            if (enclosure == null && enclosure.Animals == null)
            {
                return NotFound();
            }
            List<Animal> animals = await _context.Animal.Include(a => a.Enclosure).ToListAsync(); // List with all animals
            Collection<SelectListItem> animalSelectList = new Collection<SelectListItem>(); // List with SelectListItem

            foreach (Animal animal in animals) // Making selectlistitems for each animal
            {
                SelectListGroup group = null; // Initialize selectgroup
                if (animal.Enclosure != null) { group = new SelectListGroup { Name = animal.Enclosure.Name }; } // If the animal has an enclosure, add it to the enclosure group // Adds animals to group based on enclosure for better overview
 
                bool animalSelected = enclosure.Animals.Any(a => a.Id == animal.Id); // Checks if animal should be selected
                animalSelectList.Add(new SelectListItem() { Value = animal.Id.ToString(), Text = animal.Name, Group = group, Selected = animalSelected }); // Add the animal to the list
            }

            ViewBag.animalsList = animalSelectList; // Sets the viewbag

            return View(enclosure);
        }

        // POST: Enclosures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Climate,Habitat,SecurityLevel,Size")] Enclosure enclosure, List<string> Animals)
        {
            if (id != enclosure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    enclosure = await _context.Enclosure.Include("Animals").FirstOrDefaultAsync(c => c.Id == id); // Includes the animals

                    foreach (Animal animal in enclosure.Animals)
                    {
                        animal.EnclosureId = null; // Removes the enclosure from all the animals currently in the enclosure
                    }

                    foreach (string animalId in Animals)
                    {
                        int animalIdInt = Int32.Parse(animalId);
                        Animal animal = await _context.Animal.FindAsync(animalIdInt);

                        animal.EnclosureId = id; // Puts all the selected animals in the enclosure
                    }

                    _context.Update(enclosure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnclosureExists(enclosure.Id))
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
            return View(enclosure);
        }

        // GET: Enclosures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enclosure = await _context.Enclosure
                .Include(c => c.Animals)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enclosure == null)
            {
                return NotFound();
            }
            return View(enclosure);
        }

        // POST: Enclosures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enclosure = await _context.Enclosure.FindAsync(id);
            if (enclosure != null)
            {
                _context.Enclosure.Remove(enclosure);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnclosureExists(int id)
        {
            return _context.Enclosure.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Sunrise(int id)
        {
            var enclosure = await _context.Enclosure.Include(e => e.Animals)
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (enclosure == null)
            {
                return NotFound();
            }

            List<string> Sunrise = new List<string>();
            foreach (Animal animal in enclosure.Animals)
            {
                string Result = animal.Sunrise();
                Sunrise.Add(Result);
            }

            ViewBag.animals = await _context.Animal.Where(a => a.EnclosureId == id).ToListAsync();
            ViewBag.messages = Sunrise;

            return View("Details", enclosure);
        }
        public async Task<IActionResult> Sunset(int id)
        {
            var enclosure = await _context.Enclosure.Include(e => e.Animals)
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (enclosure == null)
            {
                return NotFound();
            }

            List<string> Sunset = new List<string>();
            foreach (Animal animal in enclosure.Animals)
            {
                string Result = animal.Sunset();
                Sunset.Add(Result);
            }

            ViewBag.animals = await _context.Animal.Where(a => a.EnclosureId == id).ToListAsync();
            ViewBag.messages = Sunset;

            return View("Details", enclosure);
        }
        public async Task<IActionResult> FeedingTime(int id)
        {
            var enclosure = await _context.Enclosure.Include(e => e.Animals)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enclosure == null)
            {
                return NotFound();
            }

            List<string> FeedingTime = new List<string>();
            foreach (Animal animal in enclosure.Animals)
            {
                Category prey = await _context.Category.FirstOrDefaultAsync(c => c.Id == animal.Prey);
                string Result = animal.FeedingTime(animal.Enclosure, prey, animal);
                FeedingTime.Add(Result);
            }

            ViewBag.animals = await _context.Animal.Where(a => a.EnclosureId == id).ToListAsync();
            ViewBag.messages = FeedingTime;

            return View("Details", enclosure);
        }
        public async Task<IActionResult> CheckConstraint(int id)
        {
            var enclosure = await _context.Enclosure.Include(e => e.Animals)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enclosure == null)
            {
                return NotFound();
            }

            List<string> CheckConstraint = new List<string>();
            foreach (Animal animal in enclosure.Animals)
            {
                string Result = animal.CheckConstraint(enclosure);
                CheckConstraint.Add(Result);
            }

            ViewBag.animals = await _context.Animal.Where(a => a.EnclosureId == id).ToListAsync();
            ViewBag.messages = CheckConstraint;

            return View("Details", enclosure);
        }
    }
}

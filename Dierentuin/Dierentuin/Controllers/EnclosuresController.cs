﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dierentuin.Data;
using Dierentuin.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

            var enclosure = await _context.Enclosure.FindAsync(id);
            if (enclosure == null)
            {
                return NotFound();
            }
            return View(enclosure);
        }

        // POST: Enclosures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Climate,Habitat,SecurityLevel,Size")] Enclosure enclosure)
        {
            if (id != enclosure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                string Result = animal.FeedingTime(enclosure, animal.Category);
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

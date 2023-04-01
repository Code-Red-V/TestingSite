using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Testing.Models;

namespace Testing.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationContext _context;

        public CategoriesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(Category cat)
        {
            //ViewData["TestId"] = new SelectList(_context.Tests, "CatId", "Name");
            //if (id == null)
            //{
            //    //return _context.Categories != null ?
            //    //          View(await _context.Categories.ToListAsync()) :
            //    //          Problem("Entity set 'ApplicationContext.Categories'  is null.");
            //}
            //else
            //{
            //    return PartialView(await _context.Categories.FindAsync(id));
            //}
            ViewBag.Categories = _context.Categories.ToList();
            return _context.Categories != null ?
                View(await _context.Categories.ToListAsync()) :
                Problem("Entity set 'ApplicationContext.Categories'  is null.");
        }

        public async Task<IActionResult> Filter(Category cat,int id)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            if (id == null)
            {

                return View();
            }
            else
            {
                return PartialView(await _context.Categories.FindAsync(id));
            }
            
        }


        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name")] Category category)
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
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
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
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}

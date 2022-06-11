using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using Web6.Data;
using Web6.Models;

namespace Web6.Controllers
{
    public class ForumCategoriesController : Controller
    {
        private readonly Web6Context _context;

        public ForumCategoriesController(Web6Context context)
        {
            _context = context;
        }

        // GET: ForumCategories
        public async Task<IActionResult> Index()
        {
              return _context.ForumCategory != null ? 
                          View(await _context.ForumCategory.ToListAsync()) :
                          Problem("Entity set 'Web6Context.ForumCategory'  is null.");
        }

        // GET: ForumCategories/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ForumCategory == null)
            {
                return NotFound();
            }

            var forumCategory = await _context.ForumCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumCategory == null)
            {
                return NotFound();
            }

            return View(forumCategory);
        }
        [Authorize(Roles = "admin")]
        // GET: ForumCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ForumCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,CategoryDescription")] ForumCategory forumCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forumCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forumCategory);
        }

        [Authorize(Roles = "admin")]
        // GET: ForumCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ForumCategory == null)
            {
                return NotFound();
            }

            var forumCategory = await _context.ForumCategory.FindAsync(id);
            if (forumCategory == null)
            {
                return NotFound();
            }
            return View(forumCategory);
        }

        // POST: ForumCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,CategoryDescription")] ForumCategory forumCategory)
        {
            if (id != forumCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumCategoryExists(forumCategory.Id))
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
            return View(forumCategory);
        }

        [Authorize(Roles = "admin")]
        // GET: ForumCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ForumCategory == null)
            {
                return NotFound();
            }

            var forumCategory = await _context.ForumCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumCategory == null)
            {
                return NotFound();
            }

            return View(forumCategory);
        }
        [Authorize(Roles = "admin")]
        // POST: ForumCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ForumCategory == null)
            {
                return Problem("Entity set 'Web6Context.ForumCategory'  is null.");
            }
            var forumCategory = await _context.ForumCategory.FindAsync(id);
            if (forumCategory != null)
            {
                _context.ForumCategory.Remove(forumCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumCategoryExists(int id)
        {
          return (_context.ForumCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

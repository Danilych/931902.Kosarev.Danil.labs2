using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web5.Data;
using Web5.Models;

namespace Web5.Controllers
{
    public class LabModelsController : Controller
    {
        private readonly Web5Context _context;

        public LabModelsController(Web5Context context)
        {
            _context = context;
        }

        // GET: LabModels
        public async Task<IActionResult> Index()
        {
              return _context.LabModel != null ? 
                          View(await _context.LabModel.ToListAsync()) :
                          Problem("Entity set 'Web5Context.LabModel'  is null.");
        }

        // GET: LabModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LabModel == null)
            {
                return NotFound();
            }

            var labModel = await _context.LabModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labModel == null)
            {
                return NotFound();
            }

            return View(labModel);
        }

        // GET: LabModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LabModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,address,phones")] LabModel labModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labModel);
        }

        // GET: LabModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LabModel == null)
            {
                return NotFound();
            }

            var labModel = await _context.LabModel.FindAsync(id);
            if (labModel == null)
            {
                return NotFound();
            }
            return View(labModel);
        }

        // POST: LabModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,address,phones")] LabModel labModel)
        {
            if (id != labModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabModelExists(labModel.Id))
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
            return View(labModel);
        }

        // GET: LabModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LabModel == null)
            {
                return NotFound();
            }

            var labModel = await _context.LabModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labModel == null)
            {
                return NotFound();
            }

            return View(labModel);
        }

        // POST: LabModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LabModel == null)
            {
                return Problem("Entity set 'Web5Context.LabModel'  is null.");
            }
            var labModel = await _context.LabModel.FindAsync(id);
            if (labModel != null)
            {
                _context.LabModel.Remove(labModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabModelExists(int id)
        {
          return (_context.LabModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

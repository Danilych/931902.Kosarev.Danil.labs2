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
    public class HospitalModelsController : Controller
    {
        private readonly Web5Context _context;

        public HospitalModelsController(Web5Context context)
        {
            _context = context;
        }

        // GET: HospitalModels
        public async Task<IActionResult> Index()
        {
              return _context.HospitalModel != null ? 
                          View(await _context.HospitalModel.ToListAsync()) :
                          Problem("Entity set 'Web5Context.HospitalModel'  is null.");
        }

        // GET: HospitalModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HospitalModel == null)
            {
                return NotFound();
            }

            var hospitalModel = await _context.HospitalModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospitalModel == null)
            {
                return NotFound();
            }

            return View(hospitalModel);
        }

        // GET: HospitalModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HospitalModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,address,phones")] HospitalModel hospitalModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospitalModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hospitalModel);
        }

        // GET: HospitalModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HospitalModel == null)
            {
                return NotFound();
            }

            var hospitalModel = await _context.HospitalModel.FindAsync(id);
            if (hospitalModel == null)
            {
                return NotFound();
            }
            return View(hospitalModel);
        }

        // POST: HospitalModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,address,phones")] HospitalModel hospitalModel)
        {
            if (id != hospitalModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospitalModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalModelExists(hospitalModel.Id))
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
            return View(hospitalModel);
        }

        // GET: HospitalModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HospitalModel == null)
            {
                return NotFound();
            }

            var hospitalModel = await _context.HospitalModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospitalModel == null)
            {
                return NotFound();
            }

            return View(hospitalModel);
        }

        // POST: HospitalModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HospitalModel == null)
            {
                return Problem("Entity set 'Web5Context.HospitalModel'  is null.");
            }
            var hospitalModel = await _context.HospitalModel.FindAsync(id);
            if (hospitalModel != null)
            {
                _context.HospitalModel.Remove(hospitalModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalModelExists(int id)
        {
          return (_context.HospitalModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

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
    public class PatientModelsController : Controller
    {
        private readonly Web5Context _context;

        public PatientModelsController(Web5Context context)
        {
            _context = context;
        }

        // GET: PatientModels
        public async Task<IActionResult> Index()
        {
              return _context.PatientModel != null ? 
                          View(await _context.PatientModel.ToListAsync()) :
                          Problem("Entity set 'Web5Context.PatientModel'  is null.");
        }

        // GET: PatientModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PatientModel == null)
            {
                return NotFound();
            }

            var patientModel = await _context.PatientModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientModel == null)
            {
                return NotFound();
            }

            return View(patientModel);
        }

        // GET: PatientModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,hospitalAddress,phones,healingDoctors")] PatientModel patientModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientModel);
        }

        // GET: PatientModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientModel == null)
            {
                return NotFound();
            }

            var patientModel = await _context.PatientModel.FindAsync(id);
            if (patientModel == null)
            {
                return NotFound();
            }
            return View(patientModel);
        }

        // POST: PatientModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,hospitalAddress,phones,healingDoctors")] PatientModel patientModel)
        {
            if (id != patientModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientModelExists(patientModel.Id))
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
            return View(patientModel);
        }

        // GET: PatientModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PatientModel == null)
            {
                return NotFound();
            }

            var patientModel = await _context.PatientModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientModel == null)
            {
                return NotFound();
            }

            return View(patientModel);
        }

        // POST: PatientModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientModel == null)
            {
                return Problem("Entity set 'Web5Context.PatientModel'  is null.");
            }
            var patientModel = await _context.PatientModel.FindAsync(id);
            if (patientModel != null)
            {
                _context.PatientModel.Remove(patientModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientModelExists(int id)
        {
          return (_context.PatientModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

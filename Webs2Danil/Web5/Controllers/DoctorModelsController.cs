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
    public class DoctorModelsController : Controller
    {
        private readonly Web5Context _context;

        public DoctorModelsController(Web5Context context)
        {
            _context = context;
        }

        // GET: DoctorModels
        public async Task<IActionResult> Index()
        {
              return _context.DoctorModel != null ? 
                          View(await _context.DoctorModel.ToListAsync()) :
                          Problem("Entity set 'Web5Context.DoctorModel'  is null.");
        }

        // GET: DoctorModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DoctorModel == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.DoctorModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctorModel == null)
            {
                return NotFound();
            }

            return View(doctorModel);
        }

        // GET: DoctorModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DoctorModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,workAdress,phones")] DoctorModel doctorModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctorModel);
        }

        // GET: DoctorModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DoctorModel == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.DoctorModel.FindAsync(id);
            if (doctorModel == null)
            {
                return NotFound();
            }
            return View(doctorModel);
        }

        // POST: DoctorModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,workAdress,phones")] DoctorModel doctorModel)
        {
            if (id != doctorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorModelExists(doctorModel.Id))
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
            return View(doctorModel);
        }

        // GET: DoctorModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DoctorModel == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.DoctorModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctorModel == null)
            {
                return NotFound();
            }

            return View(doctorModel);
        }

        // POST: DoctorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DoctorModel == null)
            {
                return Problem("Entity set 'Web5Context.DoctorModel'  is null.");
            }
            var doctorModel = await _context.DoctorModel.FindAsync(id);
            if (doctorModel != null)
            {
                _context.DoctorModel.Remove(doctorModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorModelExists(int id)
        {
          return (_context.DoctorModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

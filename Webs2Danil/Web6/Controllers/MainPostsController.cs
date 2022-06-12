using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web6.Data;
using Web6.Models;
using System.Security.Claims;

namespace Web6.Controllers
{
    public class MainPostsController : Controller
    {
        private readonly Web6Context _context;

        public MainPostsController(Web6Context context)
        {
            _context = context;
        }

        // GET: MainPosts
        public async Task<IActionResult> Index()
        {
              return _context.MainPost != null ? 
                          View(await _context.MainPost.ToListAsync()) :
                          Problem("Entity set 'Web6Context.MainPost'  is null.");
        }

        // GET: MainPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MainPost == null)
            {
                return NotFound();
            }

            var mainPost = await _context.MainPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainPost == null)
            {
                return NotFound();
            }

            return View(mainPost);
        }

        // GET: MainPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TopicMessage1,TopicMessage2")] MainPost mainPost)
        {
            if (ModelState.IsValid)
            {
                string creatorName = ((ClaimsIdentity)User.Identity).Claims.ElementAt(0).Value.ToString();

                mainPost.TopicCreator = creatorName;

                string weekDay = DateTime.Now.DayOfWeek.ToString();

                string month = "";
                switch (DateTime.Now.Month)
                {
                    case 1: month = "January"; break;
                    case 2: month = "February"; break;
                    case 3: month = "March"; break;
                    case 4: month = "April"; break;
                    case 5: month = "May"; break;
                    case 6: month = "June"; break;
                    case 7: month = "July"; break;
                    case 8: month = "August"; break;
                    case 9: month = "September"; break;
                    case 10: month = "October"; break;
                    case 11: month = "November"; break;
                    case 12: month = "December"; break;
                }

                string monthDay = DateTime.Now.Day.ToString();
                string year = DateTime.Now.Year.ToString();

                string timeTemp = DateTime.Now.TimeOfDay.ToString();
                string time = "";
                for (int i = 0; i < 8; i++) time += timeTemp[i];

                mainPost.TopicCreationDate = weekDay + ", " + month + " " + monthDay + ", " + year + " " + time;
                //topic.RepliesAmount = 0;

                mainPost.TopicCreator = creatorName;
                mainPost.TopicEditDate = mainPost.TopicCreationDate;

       
                _context.Add(mainPost);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(mainPost);
        }

        // GET: MainPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MainPost == null)
            {
                return NotFound();
            }

            var mainPost = await _context.MainPost.FindAsync(id);
            if (mainPost == null)
            {
                return NotFound();
            }
            return View(mainPost);
        }

        // POST: MainPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TopicMessage1,TopicMessage2")] MainPost mainPost)
        {
            if (id != mainPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    MainPost postDb = _context.MainPost.Where(f => f.Id == mainPost.Id).Single();
                    postDb.TopicMessage1 = mainPost.TopicMessage1;
                    postDb.TopicMessage2 = mainPost.TopicMessage2;

                    string weekDay = DateTime.Now.DayOfWeek.ToString();

                    string month = "";
                    switch (DateTime.Now.Month)
                    {
                        case 1: month = "January"; break;
                        case 2: month = "February"; break;
                        case 3: month = "March"; break;
                        case 4: month = "April"; break;
                        case 5: month = "May"; break;
                        case 6: month = "June"; break;
                        case 7: month = "July"; break;
                        case 8: month = "August"; break;
                        case 9: month = "September"; break;
                        case 10: month = "October"; break;
                        case 11: month = "November"; break;
                        case 12: month = "December"; break;
                    }

                    string monthDay = DateTime.Now.Day.ToString();
                    string year = DateTime.Now.Year.ToString();

                    string timeTemp = DateTime.Now.TimeOfDay.ToString();
                    string time = "";
                    for (int i = 0; i < 8; i++) time += timeTemp[i];

                    postDb.TopicEditDate = weekDay + ", " + month + " " + monthDay + ", " + year + " " + time;

                    _context.Update(postDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainPostExists(mainPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(mainPost);
        }

        // GET: MainPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MainPost == null)
            {
                return NotFound();
            }

            var mainPost = await _context.MainPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainPost == null)
            {
                return NotFound();
            }

            return View(mainPost);
        }

        // POST: MainPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MainPost == null)
            {
                return Problem("Entity set 'Web6Context.MainPost'  is null.");
            }
            var mainPost = await _context.MainPost.FindAsync(id);
            if (mainPost != null)
            {
                _context.MainPost.Remove(mainPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool MainPostExists(int id)
        {
          return (_context.MainPost?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

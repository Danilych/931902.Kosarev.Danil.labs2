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
    public class TopicsController : Controller
    {
        private readonly Web6Context _context;

        public TopicsController(Web6Context context)
        {
            _context = context;
        }
        

        // GET: Topics/Create
        public IActionResult Create(int? forumId)
        {
            ViewBag.ForumId = forumId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicName,ForumCategoryId")] Topic topic)
        {
            if (!ModelState.IsValid) return View(topic);

            string creatorName = ((ClaimsIdentity)User.Identity).Claims.ElementAt(0).Value.ToString();

            // if (((ClaimsIdentity)User.Identity).Claims.ElementAt(1).Value == "admin")

            topic.TopicCreator = creatorName;

            string weekDay = DateTime.Now.DayOfWeek.ToString();

            string month = "";
            switch(DateTime.Now.Month)
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

            topic.TopicCreationDate = weekDay+", "+month +" "+monthDay+", "+year+" "+time;
            topic.RepliesAmount = 0;

            topic.LasReplyCreator = creatorName;
            topic.LastReplyDate = topic.TopicCreationDate;

            ForumCategory forum = _context.ForumCategory.Where(f => f.Id == topic.ForumCategoryId).Single();

            topic.ForumCategory = forum;

            _context.Add(topic);
            await _context.SaveChangesAsync();
            // < a asp - controller = "Mockups" asp - action = "SingleForum" asp - route - id = "@ViewBag.ForumId" > Back to forum</ a >
            return RedirectToAction("SingleForum", "Mockups", new {id = topic.ForumCategoryId});     
        }

        // GET: TopicPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Topic == null)
            {
                return NotFound();
            }

            var Topic = await _context.Topic.FindAsync(id);
            if (Topic == null)
            {
                return NotFound();
            }

            if (_context.Topic == null)
                return Problem("Entity set 'Web6Context.Topic'  is null.");

            ViewBag.TopicId = id;
            return View(Topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TopicName")] Topic topic)
        {
            if (id != topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Topic topicDb = _context.Topic.Where(f => f.Id == topic.Id).Single();
                    topicDb.TopicName = topic.TopicName;

                    _context.Update(topicDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicPostExists(topic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("SingleTopic", "Mockups", new { id = topic.Id });
            }
            return View(topic);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Topic == null)
            {
                return NotFound();
            }

            var topic = await _context.Topic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }
            ViewBag.TopicId = id;
            return View(topic);
        }

        // POST: TopicPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Topic == null)
            {
                return Problem("Entity set 'Web6Context.TopicPost'  is null.");
            }
            var topic = await _context.Topic.FindAsync(id);
            if (topic != null)
            {
                _context.Topic.Remove(topic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Mockups");
        }

        private bool TopicPostExists(int id)
        {
          return (_context.Topic?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

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
    public class TopicPostsController : Controller
    {
        private readonly Web6Context _context;

        public TopicPostsController(Web6Context context)
        {
            _context = context;
        }

        // GET: TopicPosts/Reply
        public IActionResult Reply(int? TopicId)
        {
            ViewBag.TopicId = TopicId;
            return View();
        }

        // POST: TopicPosts/Reply
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply([Bind("Id,TopicMessage1,TopicMessage2, TopicId")] TopicPost topicPost)
        {
            if (!ModelState.IsValid) return View(topicPost);

            string creatorName = ((ClaimsIdentity)User.Identity).Claims.ElementAt(0).Value.ToString();

            topicPost.TopicCreator = creatorName;

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

            topicPost.TopicCreationDate = weekDay + ", " + month + " " + monthDay + ", " + year + " " + time;
            //topic.RepliesAmount = 0;

            topicPost.TopicCreator = creatorName;
            topicPost.TopicEditDate = topicPost.TopicCreationDate;

            Topic topic = _context.Topic.Where(f => f.Id == topicPost.TopicId).Single();

            topic.RepliesAmount = _context.TopicPost.Where(f => f.TopicId == topicPost.TopicId).Count() + 1;
            topic.LastReplyDate = topicPost.TopicEditDate;
            topic.LasReplyCreator = creatorName;

            _context.Add(topicPost);
            _context.Update(topic);
            await _context.SaveChangesAsync();
            // < a asp - controller = "Mockups" asp - action = "SingleForum" asp - route - id = "@ViewBag.ForumId" > Back to forum</ a >
            return RedirectToAction("SingleTopic", "Mockups", new { id = topic.Id });
           
        }

        // GET: TopicPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TopicPost == null)
            {
                return NotFound();
            }

            var topicPost = await _context.TopicPost.FindAsync(id);
            if (topicPost == null)
            {
                return NotFound();
            }
            Topic topic = _context.Topic.Where(f => f.Id == topicPost.TopicId).Single();
            ViewBag.TopicId = topic.Id;
            return View(topicPost);
        }

        // POST: TopicPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TopicMessage1,TopicMessage2,TopicId")] TopicPost topicPost)
        {
            if (id != topicPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TopicPost postDb = _context.TopicPost.Where(f => f.Id == topicPost.Id).Single();
                    postDb.TopicMessage1 = topicPost.TopicMessage1;
                    postDb.TopicMessage2 = topicPost.TopicMessage2;

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
                    if (!TopicPostExists(topicPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("SingleTopic", "Mockups", new { id = topicPost.TopicId });
            }
            return View(topicPost);
        }

        // GET: TopicPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TopicPost == null)
            {
                return NotFound();
            }

            var topicPost = await _context.TopicPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topicPost == null)
            {
                return NotFound();
            }

            return View(topicPost);
        }

        // POST: TopicPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int? TopicId = 0;
            if (_context.TopicPost == null)
            {
                return Problem("Entity set 'Web6Context.TopicPost'  is null.");
            }
            var topicPost = await _context.TopicPost.FindAsync(id);
            if (topicPost != null)
            {
                TopicId = topicPost.TopicId;
                _context.TopicPost.Remove(topicPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("SingleTopic", "Mockups", new { id = TopicId });
        }

        private bool TopicPostExists(int id)
        {
          return (_context.TopicPost?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

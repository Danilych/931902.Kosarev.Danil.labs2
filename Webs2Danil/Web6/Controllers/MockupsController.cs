using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web6.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web6.Data;

namespace Web6.Controllers
{
    public class MockupsController : Controller
    {
        private readonly Web6Context _context;

        public MockupsController(Web6Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET: ForumCategories
        public async Task<IActionResult> AllForums()
        {
            return _context.ForumCategory != null ?
                          View(await _context.ForumCategory.ToListAsync()) :
                          Problem("Entity set 'Web6Context.ForumCategory'  is null.");

        }

        //public IActionResult SingleForum()
        //{
        //    return View();
        //}

        //Next create TopicModel (one to many)
        public async Task<IActionResult> SingleForum(int? id)
        {
            //Open selected forum
            if(_context.ForumCategory == null)
                return Problem("Entity set 'Web6Context.ForumCategory'  is null.");

            if (_context.Topic == null)
                return Problem("Entity set 'Web6Context.Topic'  is null.");

            var topicsWithSameForumID = await _context.Topic.Where(t => t.ForumCategoryId == id).ToListAsync();

            ForumCategory forum = _context.ForumCategory.Where(f => f.Id == id).Single();

            ViewBag.ForumName = forum.CategoryName;
            ViewBag.ForumDescription = forum.CategoryDescription;
            ViewBag.ForumId = forum.Id;

            return View(await _context.Topic.Where(t => t.ForumCategoryId == id).ToListAsync());

        }

        public async Task<IActionResult> SingleTopic(int? id)//topic id
        {
            //Open selected topic
            if (_context.ForumCategory == null)
                return Problem("Entity set 'Web6Context.ForumCategory'  is null.");

            if (_context.Topic == null)
                return Problem("Entity set 'Web6Context.Topic'  is null.");

            if (_context.TopicPost == null)
                return Problem("Entity set 'Web6Context.TopicPost'  is null.");

            

            Topic topic = _context.Topic.Where(f => f.Id == id).Single();
            ForumCategory forum = _context.ForumCategory.Where(f => f.Id == topic.ForumCategoryId).Single();

            var postsWithSameTopicID = await _context.TopicPost.Where(t => t.TopicId == id).ToListAsync();
           
            ViewBag.ForumName = forum.CategoryName;
            ViewBag.ForumId = forum.Id;

            ViewBag.TopicName = topic.TopicName;
            ViewBag.TopicId = topic.Id;

            return View(postsWithSameTopicID);

        }

        public IActionResult FilesSystem()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
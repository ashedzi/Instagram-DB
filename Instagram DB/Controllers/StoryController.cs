using Microsoft.AspNetCore.Mvc;
using Instagram_DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Instagram_DB.Controllers {
    public class StoryController : Controller {
        private readonly InstagramDbContext _context;

        public StoryController(InstagramDbContext context) {
            _context = context;
        }

        public IActionResult Index() {
            var stories = _context.Stories
                .Select(s => new Story {
                    StoryId = s.StoryId,
                    Caption = s.Caption,
                    Image = s.Image,
                    Timestamp = s.Timestamp,
                    PosterNavigation = s.PosterNavigation
                })
                .ToList();
            return View(stories);
        }

        public IActionResult Details(string id) {
            var story = _context.Stories
                .Where(s => s.StoryId == id)
                .Select(s => new Story {
                    StoryId = s.StoryId,
                    Caption = s.Caption,
                    Image = s.Image,
                    Timestamp = s.Timestamp,
                    PosterNavigation = s.PosterNavigation,
                    ViewerNavigation = s.ViewerNavigation
                })
                .FirstOrDefault();

            if (story == null)
                return NotFound();

            var likes = _context.StoryLikes
                .Where(l => l.StoryId == id)
                .Select(l => new StoryLike {
                    StoryId = l.StoryId,
                    Liker = l.Liker,
                    Timestamp = l.Timestamp,
                    LikerNavigation = l.LikerNavigation
                })
                .ToList();

            var viewers = new List<User>();
            if (story.ViewerNavigation != null)
                viewers.Add(story.ViewerNavigation);

            ViewBag.StoryLikes = likes;
            ViewBag.StoryViewers = viewers;

            return View(story);
        }

        [HttpPost]
        public IActionResult LikeStory(string id, int likerId) {
            var exists = _context.StoryLikes.Any(l => l.StoryId == id && l.Liker == likerId);
            if (!exists) {
                var newLike = new StoryLike {
                    StoryId = id,
                    Liker = likerId,
                    Timestamp = DateTime.Now
                };
                _context.StoryLikes.Add(newLike);
                _context.SaveChanges();
            }
            return RedirectToAction("Details", new { id });
        }

    }
}

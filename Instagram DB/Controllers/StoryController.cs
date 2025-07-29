using Microsoft.AspNetCore.Mvc;
using Instagram_DB.Models;
using System.Linq;

namespace Instagram_DB.Controllers {
    public class StoryController : Controller {
        private readonly InstagramDbContext _context;

        public StoryController ( InstagramDbContext context ) {
            _context = context;
        }

        public IActionResult Index () {
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

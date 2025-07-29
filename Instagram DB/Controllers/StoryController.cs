using Instagram_DB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Instagram_DB.Controllers {
    public class StoryController : Controller {
        private static List<Story> stories = new List<Story>();
        private static List<StoryLike> storyLikes = new List<StoryLike>();
        private static List<User> users = new List<User>();
    }
}
        private static List<Suspect> suspects = new List<Suspect>();

        public static List<Suspect> GetSuspects () {
            return suspects;
        }

        public IActionResult Index () {
            return View(suspects);
        }

        [HttpGet]
        public IActionResult Create () {
            return View();
        }

        [HttpPost]
        public IActionResult Create ( Suspect suspect ) {
            if (ModelState.IsValid) {
                suspects.Add(suspect);
                return RedirectToAction("Index");
            }

            return View(suspect);
        }

        [HttpGet]
        public IActionResult Details ( string Name ) {
            var s = suspects.FirstOrDefault(x => x.Name == Name);
            if (s == null) {
                return NotFound();
            }

            return View(s);
        }

        [HttpGet]
        public IActionResult Delete ( string name ) {
            var s = suspects.FirstOrDefault(x => x.Name == name);
            if (s == null) {
                return NotFound();
            }

            return View(s);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed ( string name ) {
            var s = suspects.FirstOrDefault(x => x.Name == name);
            if (s != null) {
                suspects.Remove(s);
            }

            return RedirectToAction("Index");
        }
    }
}

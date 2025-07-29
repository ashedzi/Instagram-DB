using Instagram_DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Instagram_DB.Controllers {
    public class StoryController : Controller {
        private static List<Story> stories = new List<Story>();
        public class Story {
            public string StoryId { get; set; }
            public string Caption { get; set; }
        }
    }
}

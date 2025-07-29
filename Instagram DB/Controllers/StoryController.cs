using Microsoft.AspNetCore.Mvc;

namespace Instagram_DB.Controllers {
    public class StoryController : Controller {
        public IActionResult Index () {
            return View();
        }
    }
}

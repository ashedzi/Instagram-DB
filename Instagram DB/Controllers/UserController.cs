using Instagram_DB.BLL;
using Instagram_DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Instagram_DB.Controllers {
    public class UserController : Controller {
        private readonly UserService _userService;

        public UserController(UserService userService) {
            _userService = userService;
        }
        public IActionResult Index () {
            List<User> users = _userService.GetUsers();
            return View(users);
        }
    }
}
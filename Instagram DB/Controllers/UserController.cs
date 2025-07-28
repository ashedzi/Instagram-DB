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
            ViewBag.Title = "User List";
            List<User> users = _userService.GetUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user) {
            if (ModelState.IsValid) {
                _userService.Add(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet]

        public IActionResult Details(int id) {
            User user = _userService.GetById(id);

            if (user == null) {
                return NotFound();
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            User user = _userService.GetById(id);
            if (user == null) {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User updatedUser) {
            if (ModelState.IsValid) {
                _userService.Update(updatedUser);
                return RedirectToAction("Index");
            }
            return View(updatedUser);
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            var user = _userService.GetById(id);
            if (user == null) {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id) {
            _userService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
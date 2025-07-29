using Instagram_DB.BLL;
using Instagram_DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Instagram_DB.Controllers {
    public class UserController : Controller {
        private readonly UserService _userService;
        //private readonly FollowerService _followerService;

        public UserController(UserService userService) {
            _userService = userService;
            //_followerService = followerService;
        }

        public IActionResult Index () {
            List<User> users = _userService.GetUsers();
            //List<Follower> follows = _followerService.GetFollowers();
            return View();
        }

        [HttpGet]
        [Route("User/Profile/{UserName}")]
        public IActionResult Profile(string UserName) {
            User user = _userService.GetUsers().FirstOrDefault(u => u.Username.Equals(UserName, StringComparison.OrdinalIgnoreCase));

            if (user == null) {
                return NotFound();
            }

            return View(user);
        }

        //[HttpGet]
        //[Route("User/Followers/{UserName}")]
        //public IActionResult Followers(string UserName) {
        //    List<User> users = _userService.GetUsers();
        //    List<Follower> follows = _followerService.GetFollowers();

        //    User user = _userService.GetUsers().FirstOrDefault(u => u.Username.Equals(UserName, StringComparison.OrdinalIgnoreCase));
        //    if (user == null) {
        //        return NotFound();
        //    }

        //    var follow = users.Join(follows,
        //        users => users.UserId,
        //        follows => follows.FollowingUserId,
        //        (u, f) => new {
        //            u.UserId,
        //            u.Username,
        //            u.ProfilePic,
        //        });
        //    return View(follow);
        //}

        [HttpGet]
        public IActionResult Following (string UserName) {
            return View();
        }
    }
}
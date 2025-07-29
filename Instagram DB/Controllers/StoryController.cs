using Instagram_DB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Instagram_DB.Controllers {
    public class StoryController : Controller {
        private static List<Story> stories = new List<Story>();
        private static List<StoryLike> storyLikes = new List<StoryLike>();
        private static List<User> users = new List<User>();

    private void EnsureTestData () {
            if (!users.Any()) {
                users.AddRange(new List<User>
                {
            new User { UserId = 1, Username = "cristiano", FirstName = "Cristiano", LastName = "Ronaldo", FullName = "Cristiano Ronaldo", Email = "cr7@email.com", Status="Active", Followers=10, Following=10 },
            new User { UserId = 2, Username = "messi", FirstName = "Lionel", LastName = "Messi", FullName = "Lionel Messi", Email = "messi@email.com", Status="Active", Followers=15, Following=20 }
        });
            }
            if (!stories.Any()) {
                stories.Add(new Story {
                    StoryId = "S1",
                    Caption = "Getting ready for the big game!",
                    Poster = 1,
                    Timestamp = DateTime.Now.AddHours(-3),
                    Image = "/images/cr_story.jpg",
                    PosterNavigation = users[0],
                    Viewer = 2,
                    ViewerNavigation = users[1]
                });
                stories.Add(new Story {
                    StoryId = "S2",
                    Caption = "Training session",
                    Poster = 2,
                    Timestamp = DateTime.Now.AddHours(-2),
                    Image = "/images/messi_story.jpg",
                    PosterNavigation = users[1]
                });
            }
            if (!storyLikes.Any()) {
                storyLikes.Add(new StoryLike {
                    StoryId = "S1",
                    Liker = 2,
                    Timestamp = DateTime.Now.AddHours(-1),
                    LikerNavigation = users[1]
                });
            }
        }
        public IActionResult Index () {
            EnsureTestData();
            return View(stories);
        }
    } 
}
        
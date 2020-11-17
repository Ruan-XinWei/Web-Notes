using MVCTest.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCTest.Controllers
{
    public class UserController : Controller
    {
        static List<User> list = new List<User>();
        // GET: User
        public ActionResult Enroll()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Enroll(User user)
        {
            list.Add(user);
            if (user.username == "admin")
            {
                return View("Show", list);
            }
            return View("Show_Common", user);
        }

        public ActionResult Show()
        {
            return View("Show_Common");
        }

        public ActionResult Modify(User user)
        {
            return View(user);
        }
    }
}

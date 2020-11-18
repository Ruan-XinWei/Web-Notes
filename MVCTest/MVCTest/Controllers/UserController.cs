using MVCTest.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCTest.Controllers
{
    public class UserController : Controller
    {
        static List<User> list = new List<User>();
        static string username;
        // GET: User
        public ActionResult Enroll()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Enroll(User user)
        {
            list.Add(user);
            list.Remove(null);
            username = user.username;
            if (user.username == "admin")
            {
                return View("Show", list);
            }
            return View("Show_Common", user);
        }

        [HttpPost]
        public ActionResult Modify(User user, int new_id, string new_username, string new_password)
        {
            list.Remove(user);
            user = list.Find(u => u.id == user.id);
            if (new_id != 0)
            {
                user.id = new_id;
            }
            if (new_username != "")
            {
                user.username = new_username;
            }
            if (new_password != "")
            {
                user.password = new_password;
            }
            list.Add(user);
            return View("Show_Common", user);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (list.Remove(list.Find(user => user.id == id)))
            {
                return View("Show", list);
            }
            return View("Error");
        }
    }
}

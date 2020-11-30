using GuestBookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        // GET: User
        static int UserId;
        static string Name;
        public ActionResult AllWords()
        {
            GetUser();
            var gb = db.Guestbooks.Where(a => a.isPass == true);
            return View(gb.OrderByDescending(a=>a.CreatedOn).ToList());
        }

        public ActionResult MyWords()
        {
            GetUser();
            var gb = db.Guestbooks.Where(a => a.User.UserId == UserId);
            return View(gb.OrderByDescending(a=>a.CreatedOn).ToList());
        }

        public ActionResult CreateWords()
        {
            GetUser();
            return View();
        }

        [HttpPost]
        public ActionResult CreateWords(Guestbook gb)
        {
            GetUser();
            if (ModelState.IsValid)
            {
                gb.User = db.Users.Where(a => a.UserId == UserId).FirstOrDefault();
                //gb.CreatedOn = System.DateTime.Now;
                db.Guestbooks.Add(gb);
                db.SaveChanges();
                return RedirectToAction("MyWords");
            }
            return View();
        }

        private void GetUser()
        {
            if(Session["UserId"] != null)
            {
                UserId = (int)Session["UserId"];
                Name = db.Users.Where(a => a.UserId == UserId).FirstOrDefault().Name;
                ViewBag.NAME = Name;
            }
            else
            {
                UserId = -1;
                Name = "默认用户";
                ViewBag.NAME = "默认用户";
            }
        }
    }
}
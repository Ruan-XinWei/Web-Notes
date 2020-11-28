using GuestBookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    /*[Authorize]*/
    public class UserController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        // GET: User
        int UserId;
        string Name;
        public ActionResult AllWords(User user)
        {
            GetUser();
            var gb = db.Guestbooks.Where(a => a.isPass == true);
            return View(gb.OrderByDescending(a=>a.CreatedOn).ToList());
        }

        public ActionResult MyWords(User user)
        {
            GetUser();
            var gb = db.Guestbooks.Where(a => a.User.UserId == UserId);
            return View(gb.OrderByDescending(a=>a.CreatedOn).ToList());
        }

        private void GetUser()
        {
            UserId = (int)Session["UserId"];
            Name = db.Users.Where(a => a.UserId == UserId).FirstOrDefault().Name;
            ViewBag.NAME = Name;
        }
    }
}
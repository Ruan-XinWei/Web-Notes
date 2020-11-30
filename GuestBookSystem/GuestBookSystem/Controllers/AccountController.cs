using GuestBookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuestBookSystem.Controllers
{
    public class AccountController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        // GET: Account

        public ActionResult LoginFirst(string ReturnUrl)
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            ModelState.Remove("Email");
            ModelState.Remove("SRole");
            if (ModelState.IsValid)
            {
                var dbUser = db.Users.Where(a => a.Name == user.Name && a.Password == user.Password).FirstOrDefault();

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.UserId.ToString(), DateTime.Now, DateTime.Now.AddMinutes(30), false, user.SRole.ToString());
                string hashTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);

                //重定向至原始请求URL上
/*                string returnUrl = FormsAuthentication.GetRedirectUrl(user.UserId.ToString(), false);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl);
                }*/

                if (dbUser != null)
                {
                    ViewBag.STATE = true;
                    if (dbUser.SRole.ToString() == "管理员")
                    {
                        Session["UserId"] = dbUser.UserId;
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (dbUser.SRole.ToString() == "普通用户")
                    {
                        Session["UserId"] = dbUser.UserId;
                        return RedirectToAction("AllWords", "User");
                    }
                }
                else
                {
                    ViewBag.STATE = false;
                }
            }
            /*ModelState.AddModelError("", "用户名或密码错误");*/

            return View();
        }

        public ActionResult Register()
        {
            List<string> L = new List<string>{ "管理员", "普通用户" };
            SelectList listItems = new SelectList(L);
            ViewData["listItems"] = listItems;
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "GuestBook");
        }
    }
}

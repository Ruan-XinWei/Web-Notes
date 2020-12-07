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
            //登录不需要填写Email和SRole，将其移除
            ModelState.Remove("Email");
            ModelState.Remove("SRole");
            if (ModelState.IsValid)
            {
                //在数据库中查找对应账号密码
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

                if (dbUser != null) //如果能找到对应账号密码
                {
                    ViewBag.STATE = true;   //记录登录状态
                    if (dbUser.SRole.ToString() == "管理员")   //如果是管理员
                    {
                        Session["UserId"] = dbUser.UserId;  //记录UserId
                        return RedirectToAction("Index", "Admin");  //跳转Admin/Index
                    }
                    else if (dbUser.SRole.ToString() == "普通用户") //如果是普通用户
                    {
                        Session["UserId"] = dbUser.UserId;
                        return RedirectToAction("AllWords", "User");    //跳转User/AllWords
                    }
                }
                else
                {
                    ViewBag.STATE = false;
                }
            }

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
            if (ModelState.IsValid) //如果状态通过
            {
                db.Users.Add(user); //在数据库中添加这个记录
                db.SaveChanges();   //保存数据库
                return RedirectToAction("Login");   //跳转到/Login
            }
            return View();  //如果不通过，则返回注册视图
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "GuestBook");
        }
    }
}

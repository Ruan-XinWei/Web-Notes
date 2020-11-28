using GuestBookSystem.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace GuestBookSystem.Controllers
{
    public class AccountController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        // GET: Account

        public ActionResult Login()
        {
            ViewBag.STATE = true;
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            ViewBag.STATE = false;
            if (ModelState.IsValid)
            {
                var dbUser = db.Users.Where(a => a.Name == user.Name && a.Password == user.Password).FirstOrDefault();
                if (dbUser != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Name, false);
                    if (dbUser.SRole.ToString() == "管理员")
                    {
                        ViewBag.STATE = true;
                        Session["UserId"] = dbUser.UserId;
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (dbUser.SRole.ToString() == "普通用户")
                    {
                        ViewBag.STATE = true;
                        Session["UserId"] = dbUser.UserId;
                        return RedirectToAction("AllWords", "User");
                    }
                }
            }
            /*ModelState.AddModelError("", "用户名或密码错误");*/
            
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}

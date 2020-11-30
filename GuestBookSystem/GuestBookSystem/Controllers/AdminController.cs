using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using GuestBookSystem.Models;

namespace GuestBookSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        static int UserId;
        static string Name;
        // GET: Admin
        public ActionResult Index()
        {
            GetUser();
            /*return View(db.Guestbooks.ToList());*/
            return View(db.Guestbooks.OrderByDescending(db => db.CreatedOn).ToList());
        }

        public ActionResult Delete(int id)
        {
            GetUser();
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            GetUser();
            var gb = db.Guestbooks.Find(id);
            db.Guestbooks.Remove(gb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CheckIndex()
        {
            GetUser();
            var gb = db.Guestbooks.Where(a => a.isPass == false);
            return View(gb.OrderByDescending(a=>a.CreatedOn).ToList());
        }
        public ActionResult CheckMessageToFind(int id)
        {
            GetUser();
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }

        [HttpPost, ActionName("CheckMessageToFind")]
        public ActionResult CheckMessage(int id)
        {
            GetUser();
            var gb = db.Guestbooks.Find(id);
            gb.isPass = true;
            db.SaveChanges();
            return RedirectToAction("CheckIndex");
        }

        private void GetUser()
        {
            if (Session["UserId"] != null)
            {
                UserId = (int)Session["UserId"];
                Name = db.Users.Where(a => a.UserId == UserId).FirstOrDefault().Name;
                ViewBag.NAME = Name;
            }
            else
            {
                UserId = -1;
                Name = "管理员";
                ViewBag.NAME = "管理员";
            }
        }
    }
}

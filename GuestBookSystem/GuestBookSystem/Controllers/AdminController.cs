using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using GuestBookSystem.Models;

namespace GuestBookSystem.Controllers
{
    public class AdminController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            /*return View(db.Guestbooks.ToList());*/
            return View(db.Guestbooks.OrderByDescending(db => db.CreatedOn).ToList());
        }

        public ActionResult Delete(int id)
        {
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var gb = db.Guestbooks.Find(id);
            db.Guestbooks.Remove(gb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CheckIndex()
        {
            var gb = db.Guestbooks.Where(a => a.isPass == false);
            return View(gb.OrderByDescending(a=>a.CreatedOn).ToList());
        }
        public ActionResult CheckMessageToFind(int id)
        {
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }

        [HttpPost, ActionName("CheckMessageToFind")]
        public ActionResult CheckMessage(int id)
        {
            var gb = db.Guestbooks.Find(id);
            gb.isPass = true;
            db.SaveChanges();
            return RedirectToAction("CheckIndex");
        }
    }
}

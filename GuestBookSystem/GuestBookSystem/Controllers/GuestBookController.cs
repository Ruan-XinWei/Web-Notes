using System.Linq;
using System.Web.Mvc;
using GuestBookSystem.Models;

namespace GuestBookSystem.Controllers
{
    public class GuestBookController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        // GET: GuestBook
        public ActionResult Index()
        {
            var gb = db.Guestbooks.Where(a => a.isPass == true);
            return View(gb.OrderByDescending(a=>a.CreatedOn).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Guestbook gb)
        {
            if (ModelState.IsValid)
            {
                //gb.CreatedOn = System.DateTime.Now;
                db.Guestbooks.Add(gb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}

using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GuestBookController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        // GET: GuestBook
        public ActionResult Index()
        {
            return View(db.Guestbooks.ToList());
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

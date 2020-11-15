using FirstAspNet.DBModels;
using System.Linq;
using System.Web.Mvc;

namespace FirstAspNet.Models
{
    public class TestEFController : Controller
    {
        // GET: Test
        DBEntities db = new DBEntities();
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
    }
}

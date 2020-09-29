using System.Web.Mvc;

namespace FirstAspNet.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View("Index2");
        }

        public string Index1()
        {
            return "测试文本";
        }
    }
}

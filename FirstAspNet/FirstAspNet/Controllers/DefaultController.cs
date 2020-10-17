using System;
using System.IO;
using System.Web;
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

        public ActionResult Login(string username, string psd)
        {
            ViewBag.a = username;
            ViewBag.b = psd;

            return View();
        }

        public ActionResult Enroll()
        {
            return View();
        }

        public ActionResult ShowInfo(
            string phone, string password, string mailbox, string verifycode,
            string sex, string birthday, string age, string nativeplace,
            string city, string education, string money, string hobbys, string resume)
        {
            ViewBag.phone = phone;
            ViewBag.password = password;
            ViewBag.mailbox = mailbox;
            ViewBag.verifycode = verifycode;
            ViewBag.sex = sex;
            ViewBag.birthday = birthday;
            ViewBag.age = age;
            ViewBag.nativeplace = nativeplace;
            ViewBag.city = city;
            ViewBag.education = education;
            ViewBag.money = money;
            ViewBag.hobbys = hobbys;
            ViewBag.resume = resume;
            string filename = "";
            Upload(ref filename);
            ViewBag.photo = filename;
            return View();
        }

        public ActionResult Upload(ref string filename)
        {
            HttpPostedFileBase file = Request.Files["photo"];
            if (file == null) return Content("上传失败");
            filename = file.FileName;
            filename = filename.Substring(0, filename.IndexOf(".")) +
            GetTimeStamp() + "." +
            filename.Substring(filename.IndexOf(".") + 1);
            var fileName = Path.Combine(Request.MapPath("~/Upload"),
            Path.GetFileName(filename));
            try
            { file.SaveAs(fileName); return Content("上传成功"); }
            catch
            { return Content("上传失败"); }
        }

        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }


        public string Index1()
        {
            return "测试文本";
        }
    }
}

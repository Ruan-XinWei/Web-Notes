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
            return View(db.Guestbooks.OrderByDescending(db => db.CreatedOn).ToList());  //通过创建时间逆序，传递给前端
        }

        public ActionResult Delete(int id)
        {
            GetUser();
            var gb = db.Guestbooks.Find(id);    //找到这条留言的ID
            return View(gb);    //返回确认删除的视图
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            GetUser();
            var gb = db.Guestbooks.Find(id);    //找到这条留言的ID
            db.Guestbooks.Remove(gb);   //在数据库中删除这条留言
            db.SaveChanges();   //保存数据库
            return RedirectToAction("Index");   //返回/Admin/Index
        }

        public ActionResult CheckIndex()
        {
            GetUser();
            var gb = db.Guestbooks.Where(a => a.isPass == false);   //获取没有通过审核的留言本
            return View(gb.OrderByDescending(a=>a.CreatedOn).ToList()); //通过创建时间逆序，并传递给前端
        }
        public ActionResult CheckMessageToFind(int id)
        {
            GetUser();
            var gb = db.Guestbooks.Find(id);    //查找到留言本ID
            return View(gb);    //返回确认界面，给管理员用户确认是否通过审核
        }

        [HttpPost, ActionName("CheckMessageToFind")]
        public ActionResult CheckMessage(int id)
        {
            GetUser();
            var gb = db.Guestbooks.Find(id);    //获取留言本ID
            gb.isPass = true;   //将通过状态设置为true
            db.SaveChanges();   //保存数据库
            return RedirectToAction("CheckIndex");  //返回到Admin/CheckIndex，显示还未通过的留言
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

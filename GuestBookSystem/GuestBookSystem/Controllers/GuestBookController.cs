﻿using System.Linq;
using System.Web.Mvc;
using GuestBookSystem.Models;

namespace GuestBookSystem.Controllers
{
    public class GuestBookController : Controller
    {
        GBSDBContext db = new GBSDBContext();   //数据库
        // GET: GuestBook
        public ActionResult Index()
        {
            var gb = db.Guestbooks.Where(a => a.isPass == true);    //如果查询到状态为通过的留言
            return View(gb.OrderByDescending(a=>a.CreatedOn).ToList()); //则根据创建留言时间进行倒序
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

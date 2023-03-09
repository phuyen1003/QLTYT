using QLTYT.Model2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
    public class BenhController : Controller
    {
        private QuanLyTramYTeEntities1 db = new QuanLyTramYTeEntities1();
        // GET: Benh
        public ActionResult ThongTinBenh()
        {
            var list = db.Benhs.ToList();         
            return View(list);
        }
        public ActionResult ThemMoiBenh()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiBenh(Benh model)
        {
            db.Benhs.Add(model);
            db.SaveChanges();
            return RedirectToAction("ThongTinBenh");    
        }
        public ActionResult SuaDoiBenh(int id)
        {
            Benh b=db.Benhs.Find(id);
            return View(b);
        }
        [HttpPost]
        public ActionResult SuaDoiBenh(Benh b)
        {
            db.Entry(b).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ThongTinBenh");
        }
        [HttpPost]
        public ActionResult XoaBenh(int id) 
        {
            Benh b = db.Benhs.Find(id);
            db.Benhs.Remove(b);
            db.SaveChanges();
            return RedirectToAction("ThongTinBenh");
        }
    }
}
using QLTYT.Model2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
    public class LoaiPhongController : Controller
    {
        private QuanLyTramYTeEntities1 db = new QuanLyTramYTeEntities1();
        // GET: LoaiPhong
        public ActionResult LoaiPhong()
        {
            var list = db.LoaiPhongs.ToList();
            return View(list);
        }
        public ActionResult ThemMoiLoaiPhong()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiLoaiPhong(LoaiPhong model)
        {
            db.LoaiPhongs.Add(model);
            db.SaveChanges();
            return RedirectToAction("LoaiPhong");
        }
        public ActionResult SuaDoiLoaiPhong(int id)
        {
            LoaiPhong lp = db.LoaiPhongs.Find(id);
            return View(lp);
        }
        [HttpPost]
        public ActionResult SuaDoiLoaiPhong(LoaiPhong lp)
        {
            db.Entry(lp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("LoaiPhong");
        }
        [HttpPost]
        public ActionResult XoaLoaiPhong(int id)
        {
            LoaiPhong lp = db.LoaiPhongs.Find(id);
            db.LoaiPhongs.Remove(lp);
            db.SaveChanges();
            return RedirectToAction("LoaiPhong");
        }
    }
}
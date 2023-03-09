using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLTYT.Model2;
using QLTYT.ViewModel;

namespace QLTYT.Controllers
{
    public class PhongController : Controller
    {
        private QuanLyTramYTeEntities1 db = new QuanLyTramYTeEntities1();
        // GET: Phong
        public ActionResult ThongTinPhong()
        {
            var list = new MutipleData2();
            list.phongs = db.Phongs.Include("LoaiPhong").ToList();
            list.loaiPhongs = db.LoaiPhongs.ToList();
            return View(list);
        }
        public ActionResult ThemPhong()
        {
            var list = new MutipleData2();
            list.phongs = db.Phongs.Include("LoaiPhong").ToList();
            list.loaiPhongs = db.LoaiPhongs.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult ThemPhong(Phong p)
        {
            db.Phongs.Add(p);
            db.SaveChanges();
            return RedirectToAction("ThongTinPhong");
        }       
        [HttpGet]
        public ActionResult SuaDoiPhong(int id)
        {
            var viewModel = new MutipleData2();
            viewModel.phongs = db.Phongs.Where(v => v.IdPhong == id).ToList();
            viewModel.loaiPhongs = db.LoaiPhongs.ToList();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult SuaDoiPhong(Phong p)
        {

            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ThongTinPhong");
        }
        [HttpPost]
        public ActionResult XoaPhong(int id)
        {
            Phong p = db.Phongs.Find(id);
            db.Phongs.Remove(p);
            db.SaveChanges();
            return RedirectToAction("ThongTinPhong");
        }
    }
}
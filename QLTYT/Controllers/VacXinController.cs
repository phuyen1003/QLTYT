using QLTYT.Model2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLTYT.ViewModel;


namespace QLTYT.Controllers
{
    public class VacXinController : Controller
    {
        private QuanLyTramYTeEntities1 db = new QuanLyTramYTeEntities1();
        // GET: VacXin
        public ActionResult VacXin()
        {
            var list = new MultipleData();
            list.vacXins = db.VacXins.Include("Benh").ToList();
            list.benhs = db.Benhs.ToList();
            return View(list);
        }
        public ActionResult ThemVacXin()
        {
            var list = new MultipleData();
            list.vacXins = db.VacXins.Include("Benh").ToList();
            list.benhs = db.Benhs.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult ThemVacXin(VacXin vx)
        {
            db.VacXins.Add(vx);
            db.SaveChanges();
            return RedirectToAction("VacXin");
        }
        public ActionResult TenBenh()
        {
            var listtenbenh=db.Benhs.ToList();          
            return Json(listtenbenh, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]       
        public ActionResult SuaDoiVacXin(int id)
        {
            var viewModel = new MultipleData();
            viewModel.vacXins = db.VacXins.Where(v => v.IdVacXin == id).ToList();
            viewModel.benhs = db.Benhs.ToList();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult SuaDoiVacXin(VacXin vx)
        {
            
            db.Entry(vx).State=System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("VacXin");           
        }
        [HttpPost]
        public ActionResult XoaVacXin(int id)
        {
            VacXin vx = db.VacXins.Find(id);
            db.VacXins.Remove(vx);
            db.SaveChanges();
            return RedirectToAction("VacXin");
        }
    }
}
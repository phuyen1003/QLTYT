using QLTYT.Models;
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
    private QLTYTDataContext context = new QLTYTDataContext();
    // GET: VacXin
    public ActionResult VacXin()
    {
      var list = new MultipleData();
      list.vacXins = from vacxin in context.VacXins
                     join benh in context.Benhs on vacxin.IdBenh equals benh.IdBenh
                     select vacxin;
      list.benhs = context.Benhs.ToList();
      return View(list);
    }
    public ActionResult ThemVacXin()
    {
      var list = new MultipleData();
      list.vacXins = from vacxin in context.VacXins
                     join benh in context.Benhs on vacxin.IdBenh equals benh.IdBenh
                     select vacxin;
      list.benhs = context.Benhs.ToList();
      return View(list);
    }
    [HttpPost]
    public ActionResult ThemVacXin(VacXin vx)
    {
      context.VacXins.InsertOnSubmit(vx);
      context.SubmitChanges();
      return RedirectToAction("VacXin");
    }
    public ActionResult TenBenh()
    {
      var listtenbenh = context.Benhs.ToList();
      return Json(listtenbenh, JsonRequestBehavior.AllowGet);
    }
    [HttpGet]
    public ActionResult SuaDoiVacXin(int id)
    {
      var viewModel = new MultipleData();
      viewModel.vacXins = context.VacXins.Where(v => v.IdVacXin == id).ToList();
      viewModel.benhs = context.Benhs.ToList();
      return View(viewModel);
    }
    [HttpPost]
    public ActionResult SuaDoiVacXin(VacXin vx)
    {
      VacXin v = context.VacXins.FirstOrDefault(x => x.IdVacXin == vx.IdVacXin);
      v.TenVacXin = vx.TenVacXin;
      v.IdBenh = vx.IdBenh;
      v.LieuLuong = vx.LieuLuong;
      v.SoLo = vx.SoLo;
      v.DonGia = vx.DonGia;
      v.DonVi = vx.DonVi;
      context.SubmitChanges();
      return RedirectToAction("VacXin");
    }
    [HttpPost]
    public ActionResult XoaVacXin(int id)
    {
      VacXin vx = context.VacXins.FirstOrDefault(x => x.IdVacXin == id);
      context.VacXins.DeleteOnSubmit(vx);
      context.SubmitChanges();
      return RedirectToAction("VacXin");
    }
  }
}
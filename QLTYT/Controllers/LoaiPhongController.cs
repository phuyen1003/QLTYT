using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  [Authorize]
  public class LoaiPhongController : Controller
  {
    private QLTYTDataContext context = new QLTYTDataContext();
    // GET: LoaiPhong
    public ActionResult LoaiPhong()
    {
      var list = context.LoaiPhongs.ToList();
      return View(list);
    }
    public ActionResult ThemMoiLoaiPhong()
    {
      return View();
    }
    [HttpPost]
    public ActionResult ThemMoiLoaiPhong(LoaiPhong model)
    {
      context.LoaiPhongs.InsertOnSubmit(model);
      context.SubmitChanges();
      return RedirectToAction("LoaiPhong");
    }
    public ActionResult SuaDoiLoaiPhong(int id)
    {
      LoaiPhong lp = context.LoaiPhongs.FirstOrDefault(x => x.IdLoaiPhong == id);
      return View(lp);
    }
    [HttpPost]
    public ActionResult SuaDoiLoaiPhong(LoaiPhong lp)
    {
      LoaiPhong lphong = context.LoaiPhongs.FirstOrDefault(x => x.IdLoaiPhong == lp.IdLoaiPhong);
      lphong.TenLoaiPhong = lp.TenLoaiPhong;
      context.SubmitChanges();
      return RedirectToAction("LoaiPhong");
    }
    [HttpPost]
    public ActionResult XoaLoaiPhong(int id)
    {
      LoaiPhong lphong = context.LoaiPhongs.FirstOrDefault(x => x.IdLoaiPhong == id);
      context.LoaiPhongs.DeleteOnSubmit(lphong);
      context.SubmitChanges();
      return RedirectToAction("LoaiPhong");
    }
  }
}
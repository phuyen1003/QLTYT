using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  public class LichTiemChungController : Controller
  {
    // GET: LichTiemChung
    private QLTYTDataContext context = new QLTYTDataContext();

    public ActionResult Index()
    {
      List<LichTiemChung> list = context.LichTiemChungs.ToList();
      ViewBag.CTC = context.ChuanTiemChungs.ToList();

      return View(list);
    }


    public ActionResult Create()
    {
      if (Request.Form.Count > 0)
      {
        LichTiemChung lich = new LichTiemChung();
        lich.NgayTiem = DateTime.Parse(Request.Form["NgayTiem"]);
        lich.IdVacXin = int.Parse(Request.Form["IdVacXin"]);
        lich.GhiChu = Request.Form["GhiChu"];
        lich.TrangThai = bool.Parse(Request.Form["NgayTiem"]);
        context.LichTiemChungs.InsertOnSubmit(lich);
        context.SubmitChanges();
        return RedirectToAction("Index");
      }
      return View();
    }



    public ActionResult Edit(int id)
    {
      LichTiemChung lich = context.LichTiemChungs.FirstOrDefault(x => x.IdLichTiemChung == id);
      ViewBag.ListVacXin = context.VacXins.ToList();
      if (lich.NgayTiem != null)
      {
        ViewBag.NgayTiem = lich.NgayTiem?.ToString("yyyy-MM-dd");
      }
     
      if (Request.Form.Count > 0)
      {
        lich.NgayTiem = DateTime.Parse(Request.Form["NgayTiem"]);
        lich.IdVacXin = int.Parse(Request.Form["IdVacXin"]);
        lich.GhiChu = Request.Form["GhiChu"];
        lich.TrangThai = bool.Parse(Request.Form["NgayTiem"]);
        context.SubmitChanges();
        return RedirectToAction("Index");
      }
      return View(lich);
    }



  }
}
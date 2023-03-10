using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  public class ChiTietPhieuTiemController : Controller
  {
    // GET: ChiTietPhieuKham
    [Authorize]
    public ActionResult Index()
    {
      return View();
    }


    public ActionResult Create()
    {
      QLTYTDataContext context = new QLTYTDataContext();

      List<PhieuTiemChung> listpt = context.PhieuTiemChungs.ToList();
      List<ChiTietTrieuChung> listc = context.ChiTietTrieuChungs.ToList();
      List<VacXin> listvc = context.VacXins.ToList();

      

      ViewBag.phieutiem = listpt;
      ViewBag.trieuchung = listc;
      ViewBag.vacxin = listvc;
      ViewBag.phanung = context.PhanUngs.ToList();

      if (Request.Form.Count > 0)
      {
        string solo = Request.Form["SoLo"].ToString();
        string diadiem = Request.Form["DiaDiem"].ToString();
        string ghichu = Request.Form["GhiChu"].ToString();
        int idptc = int.Parse(Request.Form["IdSoPhieu"]);
        int idcttc = int.Parse(Request.Form["IdPhanUng"]);
        int idvc = int.Parse(Request.Form["IdVacXin"]);

        ChiTietTiemChung sp = new ChiTietTiemChung();

        sp.SoLo = solo;
        sp.DiaDiem = diadiem;
        sp.GhiChu = ghichu;
        sp.IdSoPhieu = idptc;
        sp.IdPhanUng = idcttc;
        sp.IdVacXin = idvc;


        HttpPostedFileBase file = Request.Files["HinhAnh"];
        if (file != null)
        {
          string serverpath = HttpContext.Server.MapPath("~/img");
          string filepath = serverpath + "/" + file.FileName;
          if (file.FileName.Equals(""))
          {
            sp.HinhAnh = "";
          }
          else
          {
            file.SaveAs(filepath);
            sp.HinhAnh = file.FileName;
          }

        }
        context.SubmitChanges();
        return RedirectToAction("Index", "TaoPhieuTiemChung");
      }


      return View();
    }
  }
}
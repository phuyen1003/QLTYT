using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  public class ThongTinThaiKyController : Controller
  {
    // GET: ThongTinThaiKy
    QLTYTDataContext context = new QLTYTDataContext();
    public ActionResult Index()
    {
      QLTYTDataContext context = new QLTYTDataContext();
      List<SP_thaikyResult> tk = context.SP_thaiky().ToList();
      return View(tk);
    }


    public ActionResult Create()
    {
      QLTYTDataContext context = new QLTYTDataContext();

      List<BenhNhan> listbenhnhan = context.BenhNhans.Where(row => row.IdNhomBenhNhan == 1).ToList();



      ViewBag.benhnhan = listbenhnhan;


      if (Request.Form.Count > 0)
      {


        float tuantuoi = float.Parse(Request.Form["TuanTuoi"]);
        float chieudai = float.Parse(Request.Form["ChieuDaiThaiNhi"]);
        float cannang = float.Parse(Request.Form["CanNangThaiNhi"]);
        string ghichu = Request.Form["GhiChu"];
        int idbn = int.Parse(Request.Form["IdBenhNhan"]);




        TTTK_Me sp = new TTTK_Me();

        sp.TuanTuoi = tuantuoi;
        sp.ChieuDaiThaiNhi = chieudai;
        sp.CanNangThaiNhi = cannang;
        sp.GhiChu = ghichu;
        sp.IdBenhNhan = idbn;




        context.TTTK_Mes.InsertOnSubmit(sp);
        context.SubmitChanges();
        return RedirectToAction("Index");
      }


      return View();
    }



  }
}
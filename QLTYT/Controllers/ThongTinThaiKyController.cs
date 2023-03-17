using QLTYT.common;
using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  [Authorize]
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
        DateTime ngaydk = DateTime.Parse(Request.Form["NgayDuKham"]);
        int idbn = int.Parse(Request.Form["IdBenhNhan"]);




        TTTK_Me sp = new TTTK_Me();

        sp.TuanTuoi = tuantuoi;
        sp.ChieuDaiThaiNhi = chieudai;
        sp.CanNangThaiNhi = cannang;
        sp.GhiChu = ghichu;
        sp.NgayDuKham = ngaydk;
        sp.IdBenhNhan = idbn;




        context.TTTK_Mes.InsertOnSubmit(sp);
        context.SubmitChanges();
        return RedirectToAction("Index");
      }


      return View();
    }
    public static class MailHelper
    {
      public static void Send(MailMessage message)
      {
        using (var client = new SmtpClient())
        {
          client.Send(message);
        }
      }
    }

    public ActionResult SendEmails()
    {

      var patientData = from p in context.BenhNhans
                        join pr in context.TTTK_Mes on p.IdBenhNhan equals pr.IdBenhNhan
                        select new
                        {
                          p.Email,
                          pr.IdBenhNhan,
                          p.HoTen,
                          pr.TuanTuoi,
                          pr.CanNangThaiNhi,
                          pr.ChieuDaiThaiNhi,
                          pr.NgayDuKham
                        };


      foreach (var patient in patientData)
      {
        MailMessage message = new MailMessage();
        message.To.Add(new MailAddress(patient.Email));
        message.Subject = "Trạm Y Tế xin thông báo về thông tin thai kỳ và ngày hẹn khám thai";
        message.Body = "Thân gửi ông/bà: " + patient.HoTen +
                            "\nTuần tuổi: " + patient.TuanTuoi +
                            "\nCân nặng thai kỳ: " + patient.CanNangThaiNhi +
                            "\nChiều dài thai kỳ: " + patient.ChieuDaiThaiNhi +
                            "\nNgày dự khám: " + patient.NgayDuKham;

        MailHelper.Send(message);
      }
      TempData["ThongBao"] = "Gửi mail thành công!";
      return RedirectToAction("Index");
    }


  }



}
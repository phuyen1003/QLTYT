using QLTYT.common;
using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  [Authorize]
  public class TienIchController : Controller
  {
    // GET: TienIch
    public ActionResult Index()
    {
      return View();
    }

    //chưa test
    public ActionResult TBChichNgua()
    {
      QLTYTDataContext context = new QLTYTDataContext();
      List<BenhNhan> listbn = context.BenhNhans.ToList();
      //get ListBN

      if (listbn != null)
      {
        foreach (var bn in listbn)
        {
          //get ListIdCTC phải tiêm chủng theo bệnh nhân B1
          var listIdCTC = context.SP_ListIdCTC(bn.IdBenhNhan).ToList();
          if (listIdCTC != null && listIdCTC.Count() > 0)
          {
            //get GiaDinhBenhNhan
            var gd = context.GiaDinhs.FirstOrDefault(s => s.IdGiaDinh.Equals(bn.IdGiaDinh));
            foreach (var ctc in listIdCTC)
            {
              //get ListDaTiem của BenhNhan theo từng chuẩn
              var listDaTiem = context.SP_ListDaTiem(bn.IdBenhNhan, ctc.Id).ToList();
              //set
              if (listDaTiem.Count() !=0)
              {
                foreach (var datiem in listDaTiem)
                {
                  if (datiem.sl < ctc.SoLan)
                  {
                    string slg = (datiem.sl + 1).ToString();
                    var nearstDay = context.SP_NearstDay(bn.IdBenhNhan, datiem.IdVacXin);

                    if(nearstDay!= null)
                    {
                     foreach(var near in nearstDay) { 
                        if(DateTime.Now >= (near.NgayTao?.AddMonths((int)near.thang))?.AddDays(-1))
                        {
                          if (DateTime.Now <= near.NgayTao?.AddMonths((int)near.thang))
                          {
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/TBChichNgua.html"));
                            var benh = context.Benhs.FirstOrDefault(bb => bb.IdBenh == ctc.IdBenh);
                            var ngay = near.NgayTao?.AddMonths((int)near.thang).ToString("dd-MM-yyyy");

                            //  var ngayy = ngay.ToString("dd/MM/yyyy");
                            content = content.Replace("{{TenPH}}", gd.TenChuHo);
                            content = content.Replace("{{TenTre}}", bn.HoTen);
                            content = content.Replace("{{TenKyTiem}}", benh.TenBenh);
                            content = content.Replace("{{LanThu}}", slg);
                            content = content.Replace("{{NgayTiem}}", ngay);
                            var toGmail = gd.Email;
                            new MailHelper().SendMail(toGmail, "Thông báo đến kỳ tiêm phòng", content);
                          }
                          else
                          {
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/TBChichNgua.html"));
                            var benh = context.Benhs.FirstOrDefault(bb => bb.IdBenh == ctc.IdBenh);
                            //  var ngayy = ngay.ToString("dd/MM/yyyy");
                            content = content.Replace("{{TenPH}}", gd.TenChuHo);
                            content = content.Replace("{{TenTre}}", bn.HoTen);
                            content = content.Replace("{{TenKyTiem}}", benh.TenBenh);
                            content = content.Replace("{{LanThu}}", slg);
                            content = content.Replace("{{NgayTiem}}", "Các ngày trong tuần");
                            var toGmail = gd.Email;
                            new MailHelper().SendMail(toGmail, "Thông báo đến kỳ tiêm phòng", content);
                          }
                        }
              
                      }
                    }                  
                  }
                }
              }
              else
              {
                //set tuổi hiện tại cần tiêm gì và gửi thông báo tất cả //
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/TBChichNgua.html"));
                var benh = context.Benhs.FirstOrDefault(bb => bb.IdBenh == ctc.IdBenh);
                content = content.Replace("{{TenPH}}", gd.TenChuHo);
                content = content.Replace("{{TenTre}}", bn.HoTen);
                content = content.Replace("{{TenKyTiem}}", benh.TenBenh);
                content = content.Replace("{{LanThu}}", "đầu tiên");
                content = content.Replace("{{NgayTiem}}", "Các ngày trong tuần");
                var toGmail = gd.Email;
                new MailHelper().SendMail(toGmail, "Thông báo đến kỳ tiêm phòng", content);
              }
            }

          }
        }
        TempData["ThongBao"] = "Gửi mail thành công!";
      }    
      return RedirectToAction("Index","Home");

    }


  }
}
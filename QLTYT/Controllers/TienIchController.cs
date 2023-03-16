using QLTYT.common;
using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  public class TienIchController : Controller
  {
    // GET: TienIch
    public ActionResult Index()
    {
      return View();
    }

    //chưa test
    public string TBChichNgua()
    {
      QLTYTDataContext context = new QLTYTDataContext();
      List<BenhNhan> listbn = context.BenhNhans.ToList();


      if (listbn != null)
      {
        foreach (var bn in listbn)
        {
          var listIdCTC = context.SP_ListIdCTC(bn.IdBenhNhan).ToList();
          if (listIdCTC != null && listIdCTC.Count() > 0)
          {
            var gd = context.GiaDinhs.FirstOrDefault(s => s.IdGiaDinh.Equals(bn.IdGiaDinh));
            foreach (var ctc in listIdCTC)
            {
              var listDaTiem = context.SP_ListDaTiem(bn.IdBenhNhan, ctc.Id).ToList();
              if (listDaTiem != null)
              {
                foreach (var datiem in listDaTiem)
                {
                  if (datiem.slg <= ctc.SoLan)
                  {
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/TBChichNgua.html"));
                    var benh = context.Benhs.FirstOrDefault(bb => bb.IdBenh == ctc.IdBenh);
                    content = content.Replace("{{TenPH}}", gd.TenChuHo);
                    content = content.Replace("{{TenTre}}", bn.HoTen);
                    content = content.Replace("{{TenKyTiem}}", benh.TenBenh);
                    content = content.Replace("{{NgayTiem}}", "Các ngày trong tuần");
                    var toGmail = gd.Email;
                    new MailHelper().SendMail(toGmail, "Thông báo đến kỳ tiêm chủng", content);
                  }
                }
              }
              else
              {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/TBChichNgua.html"));
                var benh = context.Benhs.FirstOrDefault(bb => bb.IdBenh == ctc.IdBenh);
                content = content.Replace("{{TenPH}}", gd.TenChuHo);
                content = content.Replace("{{TenTre}}", bn.HoTen);
                content = content.Replace("{{TenKyTiem}}", benh.TenBenh);
                content = content.Replace("{{NgayTiem}}", "Các ngày trong tuần");
                var toGmail = gd.Email;
                new MailHelper().SendMail(toGmail, "Thông báo đến kỳ tiêm chủng", content);
              }
            }

          }




        }
      }
      return "Thành công!";

    }


  }
}
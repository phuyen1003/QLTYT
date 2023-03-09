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
    public ActionResult TBChichNgua()
    {
      if (ModelState.IsValid)
      {
        if (Request.Form.Count > 0)
        {
          QLTYTDataContext context = new QLTYTDataContext();
          List<BenhNhan> listtre = context.BenhNhans.Where(x => x.IdNhomBenhNhan == 2).ToList();
          if(listtre != null)
          {
            foreach (var tre in listtre)
            {
              var gd = context.GiaDinhs.FirstOrDefault(s => s.IdGiaDinh.Equals(tre.IdGiaDinh));

              string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/TBChichNgua.html"));

              content = content.Replace("{{TenPH}}", gd.TenChuHo);
              content = content.Replace("{{TenTre}}", tre.HoTen);
              //content = content.Replace("{{TenKyTiem}}", nv.HoTen);
              //content = content.Replace("{{NgayTiem}}", nv.HoTen);

              var toGmail = gd.Email;
              new MailHelper().SendMail(toGmail, "Thông báo đến kỳ tiêm chủng", content);
            }
          }
        
        }

      }
      return View("ViewFogot");
    }


  }
}
using QLTYT.common;
using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QLTYT.Controllers
{

  public class HomeController : Controller
  {
    [Authorize]
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult About()
    {


      return View();
    }

    public ActionResult Contact()
    {

      return View();
    }

    public ActionResult LogIn()
    {
      return View();
    }



    [HttpPost]
    public ActionResult LogIn(TaiKhoan account, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        QLTYTDataContext context = new QLTYTDataContext();
        returnUrl = (string)TempData["returnUrl"];
        string user = account.TenTK;
        string pass = account.MatKhau;
        var acc = context.TaiKhoans.FirstOrDefault(s => s.TenTK.Equals(user) && s.MatKhau.Equals(pass));
        if (acc != null)
        {
          //TempData["TaiKhoan"] = user;         
          NhanVien nv = context.NhanViens.FirstOrDefault(n => n.IdNhanVien.Equals(acc.IDNV));
          PhanQuyen pq = context.PhanQuyens.FirstOrDefault(n => n.IdQuyen.Equals(acc.IDQuyen));

          FormsAuthentication.SetAuthCookie(acc.TenTK, false);
          if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
              && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
          {
            return Redirect(returnUrl);
          }
          else
          {
            FormsAuthentication.SetAuthCookie(account.TenTK, true);
            Session["TaiKhoan"] = acc;
            TempData["User"] = nv.HoTen;
            TempData["UserIMG"] = nv.HinhAnh;
            return RedirectToAction("index", "Home");
          }
        }
        else
        {
          ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác!");
        }
      }
      return View();
    }



    //[Authorize]
    public ActionResult LogOut()
    {
      FormsAuthentication.SignOut();
      Session.Clear();
      return RedirectToAction("LogIn", "Home");
    }



    char ch;
    private string Random()
    {
      StringBuilder builder = new StringBuilder();
      Random random = new Random();

      for (int i = 0; i < 6; i++)
      {
        ch = Convert.ToChar(Convert.ToInt32(random.Next(65, 87)));
        builder.Append(ch);
      }
      return builder.ToString().ToLower();
    }


    public ActionResult ViewForgot()
    {
      return View();
    }


    [HttpPost]
    //chưa test
    public ActionResult ForgotPassword()
    {
      if (ModelState.IsValid)
      {
        if (Request.Form.Count > 0)
        {
          QLTYTDataContext context = new QLTYTDataContext();
          string username = Request.Form["TenTK"];
          var acc = context.TaiKhoans.FirstOrDefault(s => s.TenTK.Equals(username));
          if (acc != null)
          {
            var nv = context.NhanViens.FirstOrDefault(s => s.IdNhanVien.Equals(acc.IDNV));
            acc.MatKhau = Random();
            context.SubmitChanges();

            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/MailTemp.html"));

            content = content.Replace("{{CustomerName}}", acc.TenTK);
            content = content.Replace("{{Ten}}", nv.HoTen);
            content = content.Replace("{{Email}}", nv.Email);
            content = content.Replace("{{Password}}", acc.MatKhau);

            var toGmail = nv.Email;
            new MailHelper().SendMail(toGmail, "Mật khẩu mới", content);
            TempData["ThongBao"] = "Mật khẩu mới đã được gửi đến gmail của bạn!";
            return RedirectToAction("LogIn", "Home");
          }
          else
          {
            ModelState.AddModelError("", "Tài khoản không tồn tại!");
          }
        }

      }
      return View("ViewFogot");
    }



  }
}
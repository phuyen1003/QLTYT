using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  [Authorize]
    public class BenhController : Controller
    {
    // GET: Benh
    private QLTYTDataContext context = new QLTYTDataContext();
    public ActionResult ThongTinBenh()
    {
      var list = context.Benhs.ToList();
      return View(list);
    }
    public ActionResult ThemMoiBenh()
    {
      return View();
    }
    [HttpPost]
    public ActionResult ThemMoiBenh(Benh model)
    {
      context.Benhs.InsertOnSubmit(model);
      context.SubmitChanges();
      return RedirectToAction("ThongTinBenh");
    }
    public ActionResult SuaDoiBenh(int id)
    {
      Benh b = context.Benhs.FirstOrDefault(x => x.IdBenh == id);
      return View(b);
    }

    [HttpPost]
    public ActionResult SuaDoiBenh(Benh b)
    {
      Benh benh = context.Benhs.FirstOrDefault(x => x.IdBenh == b.IdBenh);
      benh.TenBenh = b.TenBenh;
      context.SubmitChanges();
      return RedirectToAction("ThongTinBenh");
    }

    [HttpPost]
    public ActionResult XoaBenh(int id)
    {
      Benh benh = context.Benhs.FirstOrDefault(x => x.IdBenh == id);
      context.Benhs.DeleteOnSubmit(benh);
      context.SubmitChanges();
      return RedirectToAction("ThongTinBenh");
    }
  }
}
using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  public class TrieuChungController : Controller
  {
    // GET: TrieuChung
    private QLTYTDataContext context = new QLTYTDataContext();
    public ActionResult Index()
    {
      List<TrieuChung> list = context.TrieuChungs.OrderBy(x => x.TenTrieuChung).ToList();
      //ViewBag.ListNNV = context.NhomNhanViens.ToList();
      return View(list);
    }

    public ActionResult Create()
    {
      if (Request.Form.Count > 0)
      {
        string ten = Request.Form["TenTrieuChung"];
        TrieuChung tc = new TrieuChung();
        tc.TenTrieuChung = ten;
        context.TrieuChungs.InsertOnSubmit(tc);
        context.SubmitChanges();
        return RedirectToAction("Index");
      }
      return View();
    }


    public ActionResult Edit(int id)
    {
      TrieuChung tc = context.TrieuChungs.FirstOrDefault(x => x.IdTrieuChung == id);

      if (Request.Form.Count > 0)
      {
        string ten = Request.Form["TenTrieuChung"];
        tc.TenTrieuChung = ten;
        context.SubmitChanges();
        return RedirectToAction("Index");
      }
      return View(tc);
    }

    public ActionResult Delete(int id)
    {
      TrieuChung tc = context.TrieuChungs.FirstOrDefault(x => x.IdTrieuChung == id);
      if (tc != null)
      {
        context.TrieuChungs.DeleteOnSubmit(tc);
        context.SubmitChanges();
      }
      return RedirectToAction("Index");
    }



  }
}
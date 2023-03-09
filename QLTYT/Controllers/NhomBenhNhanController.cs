using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  public class NhomBenhNhanController : Controller
  {
    // GET: NhomBenhNhan
    private QLTYTDataContext context = new QLTYTDataContext();

    public ActionResult Index()
    {
      List<NhomBenhNhan> list = context.NhomBenhNhans.ToList();
      return View(list);
    }


    public ActionResult Create()
    {
      if (Request.Form.Count > 0)
      {
        string ten = Request.Form["TenNhom"];
        NhomBenhNhan nhom = new NhomBenhNhan();
        nhom.TenNhom = ten;
        context.NhomBenhNhans.InsertOnSubmit(nhom);
        context.SubmitChanges();
        return RedirectToAction("Index");
      }
      return View();
    }


    public ActionResult Edit(int id)
    {
      NhomBenhNhan nhom = context.NhomBenhNhans.FirstOrDefault(x => x.IdNhomBenhNhan == id);

      if (Request.Form.Count > 0)
      {
        string ten = Request.Form["TenNhom"];
        nhom.TenNhom = ten;
        context.SubmitChanges();
        return RedirectToAction("Index");
      }
      return View(nhom);
    }

    public ActionResult Delete(int id)
    {
      NhomBenhNhan nhom = context.NhomBenhNhans.FirstOrDefault(x => x.IdNhomBenhNhan == id);
      if (nhom != null)
      {
        context.NhomBenhNhans.DeleteOnSubmit(nhom);
        context.SubmitChanges();
      }
      return RedirectToAction("Index");
    }



  }
}
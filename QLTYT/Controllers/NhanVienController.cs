using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  public class NhanVienController : Controller
  {
    // GET: NhanVien
    private QLTYTDataContext context = new QLTYTDataContext();
    public ActionResult Index()
    {
      List<NhanVien> list = context.NhanViens.OrderBy(x => x.HoTen).ToList();
      ViewBag.ListNNV = context.NhomNhanViens.ToList();
      return View(list);
    }


    public ActionResult ViewCreate()
    {
      ViewBag.ListNNV = context.NhomNhanViens.ToList();
      return View();
    }

    [HttpPost]
    public ActionResult Create()
    {
      if (Request.Form.Count > 0)
      {
        int IdNhomNhanVien = int.Parse(Request.Form["IdNhomNhanVien"]);
        string HoTen = Request.Form["HoTen"];
        string SDT = Request.Form["SDT"];
        int GioiTinh = int.Parse(Request.Form["GioiTinh"]);
        DateTime NgaySinh = DateTime.Parse(Request.Form["NgaySinh"]);
        string CCCD = Request.Form["CCCD"];
        string Email = Request.Form["Email"];
        string DiaChi = Request.Form["DiaChi"];

        NhanVien nv = new NhanVien();
        nv.IdNhomNhanVien = IdNhomNhanVien;
        nv.HoTen = HoTen;
        nv.SDT = SDT;
        nv.GioiTinh = GioiTinh;
        nv.NgaySinh = NgaySinh;
        nv.CCCD = CCCD;
        nv.Email = Email;
        nv.DiaChi = DiaChi;
        

        HttpPostedFileBase file = Request.Files["HinhAnh"];
        if (file != null)
        {
          string serverpath = HttpContext.Server.MapPath("~/imagesNV");
          string filepath = serverpath + "/" + file.FileName;
          file.SaveAs(filepath);
          nv.HinhAnh = file.FileName;
        }
        context.NhanViens.InsertOnSubmit(nv);
        context.SubmitChanges();
        return RedirectToAction("Index");
      }
      return View();
    }




    public ActionResult Edit(int id)
    {
      NhanVien nv = context.NhanViens.FirstOrDefault(x => x.IdNhanVien == id);
      if (nv.NgaySinh != null)
      {
        //ViewBag.NgaySinh = nv.NgaySinh?.ToShortDateString();
        ViewBag.NgaySinh = nv.NgaySinh?.ToString("yyyy-MM-dd");
      }
      
      ViewBag.ListNNV = context.NhomNhanViens.ToList();
      if (Request.Form.Count > 0)
      {
        int IdNhomNhanVien = int.Parse(Request.Form["IdNhomNhanVien"]);
        string HoTen = Request.Form["HoTen"];
        string SDT = Request.Form["SDT"];
        int GioiTinh = int.Parse(Request.Form["GioiTinh"]);
        DateTime NgaySinh = DateTime.Parse(Request.Form["NgaySinh"]);
        string CCCD = Request.Form["CCCD"];
        string Email = Request.Form["Email"];
        string DiaChi = Request.Form["DiaChi"];

        nv.IdNhomNhanVien = IdNhomNhanVien;
        nv.HoTen = HoTen;
        nv.SDT = SDT;
        nv.GioiTinh = GioiTinh;
        nv.NgaySinh = NgaySinh;
        nv.CCCD = CCCD;
        nv.Email = Email;
        nv.DiaChi = DiaChi;

        context.SubmitChanges();
        return RedirectToAction("Index");
      }
      return View(nv);
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
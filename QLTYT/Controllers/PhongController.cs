using QLTYT.Models;
using QLTYT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace QLTYT.Controllers
{
  public class PhongController : Controller
  {
    // GET: Phong
    private QLTYTDataContext context = new QLTYTDataContext();

    public ActionResult ThongTinPhong()
    {
      var list = new MutipleData2();
      list.phongs = (IEnumerable<Phong>)(from phong in context.Phongs
                     join loaiPhong in context.LoaiPhongs on phong.IdLoaiPhong equals loaiPhong.IdLoaiPhong
                     select new
                     {
                       Phong = phong,
                       LoaiPhong = loaiPhong
                     });
      return View(list);
    }
    public ActionResult ThemPhong()
    {
      var list = new MutipleData2();
      list.phongs = (IEnumerable<Phong>)(from phong in context.Phongs
                     join loaiPhong in context.LoaiPhongs on phong.IdLoaiPhong equals loaiPhong.IdLoaiPhong
                     select new
                     {
                       Phong = phong,
                       LoaiPhong = loaiPhong
                     }).AsEnumerable();
      return View(list);
    }
    [HttpPost]
    public ActionResult ThemPhong(Phong p)
    {
      context.Phongs.InsertOnSubmit(p);
      context.SubmitChanges();
      return RedirectToAction("ThongTinPhong");
    }
    [HttpGet]
    public ActionResult SuaDoiPhong(int id)
    {
      var viewModel = new MutipleData2();
      viewModel.phongs = context.Phongs.Where(v => v.IdPhong == id).ToList();
      viewModel.loaiPhongs = context.LoaiPhongs.ToList();
      return View(viewModel);
    }
    [HttpPost]
    public ActionResult SuaDoiPhong(Phong p)
    {
      Phong phong = context.Phongs.FirstOrDefault(x => x.IdPhong == p.IdPhong);
      phong.TenPhong = p.TenPhong;
      phong.LoaiPhong = p.LoaiPhong;
      phong.TinhTrang = p.TinhTrang;
      context.SubmitChanges();
      return RedirectToAction("ThongTinPhong");
    }
    [HttpPost]
    public ActionResult XoaPhong(int id)
    {
      Phong p = context.Phongs.FirstOrDefault(x => x.IdPhong == id);
      context.Phongs.DeleteOnSubmit(p);
      context.SubmitChanges();
      return RedirectToAction("ThongTinPhong");
    }
  }
}
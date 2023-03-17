using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLTYT.ViewModel;

namespace QLTYT.Controllers
{
  [Authorize]
  public class TaoPhieuKhamController : Controller
  {
    private QLTYTDataContext context = new QLTYTDataContext();
    // GET: TaoPhieu
    public ActionResult PhieuKham()
    {
      var list = new MutipleData2();
      list.phieuKhams = context.PhieuKhams
                        .Join(context.NhanViens, pk => pk.IdNhanVien, nv => nv.IdNhanVien, (pk, nv) => new { PhieuKham = pk, NhanVien = nv })
                        .Join(context.Phongs, x => x.PhieuKham.IdPhong, p => p.IdPhong, (x, p) => new { x.PhieuKham, x.NhanVien, Phong = p })
                        .Join(context.BenhNhans, x => x.PhieuKham.IdBenhNhan, bn => bn.IdBenhNhan, (x, bn) => new { x.PhieuKham, x.NhanVien, x.Phong, BenhNhan = bn })
                        .Join(context.ThongTinThaiKies, x => x.PhieuKham.IdThongTinThaiKy, ttk => ttk.IdThongTinThaiKy, (x, ttk) => new { x.PhieuKham, x.NhanVien, x.Phong, x.BenhNhan, ThongTinThaiKy = ttk })
                        .Join(context.LoaiPhongs, x => x.Phong.IdLoaiPhong, lp => lp.IdLoaiPhong, (x, lp) => new { x.PhieuKham, x.NhanVien, Phong = x.Phong, x.BenhNhan, x.ThongTinThaiKy, LoaiPhong = lp })
                        .Select(x => x.PhieuKham);

      list.nhanViens = context.NhanViens
                      .Join(context.NhomNhanViens, nv => nv.IdNhomNhanVien, nnv => nnv.IdNhomNhanVien, (nv, nnv) => new { NhanVien = nv, NhomNhanVien = nnv })
                      .Select(x => x.NhanVien);

      list.phongs = from phong in context.Phongs
                    join loaiPhong in context.LoaiPhongs on phong.IdLoaiPhong equals loaiPhong.IdLoaiPhong
                    select phong;
      list.benhNhans = context.BenhNhans
                .Join(context.NhomBenhNhans, bn => bn.IdNhomBenhNhan, nb => nb.IdNhomBenhNhan, (bn, nb) => new { BenhNhan = bn, NhomBenhNhan = nb })
                .Select(x => x.BenhNhan);
      list.thongTinThaiKies = context.ThongTinThaiKies.ToList();
      return View(list);
    }
    public ActionResult TaoPhieuKham()
    {
      var list = new MutipleData2();
      list.phieuKhams = context.PhieuKhams
                        .Join(context.NhanViens, pk => pk.IdNhanVien, nv => nv.IdNhanVien, (pk, nv) => new { PhieuKham = pk, NhanVien = nv })
                        .Join(context.Phongs, x => x.PhieuKham.IdPhong, p => p.IdPhong, (x, p) => new { x.PhieuKham, x.NhanVien, Phong = p })
                        .Join(context.BenhNhans, x => x.PhieuKham.IdBenhNhan, bn => bn.IdBenhNhan, (x, bn) => new { x.PhieuKham, x.NhanVien, x.Phong, BenhNhan = bn })
                        .Join(context.ThongTinThaiKies, x => x.PhieuKham.IdThongTinThaiKy, ttk => ttk.IdThongTinThaiKy, (x, ttk) => new { x.PhieuKham, x.NhanVien, x.Phong, x.BenhNhan, ThongTinThaiKy = ttk })
                        .Join(context.LoaiPhongs, x => x.Phong.IdLoaiPhong, lp => lp.IdLoaiPhong, (x, lp) => new { x.PhieuKham, x.NhanVien, Phong = x.Phong, x.BenhNhan, x.ThongTinThaiKy, LoaiPhong = lp })
                        .Select(x => x.PhieuKham);
      list.nhanViens = context.NhanViens
                      .Join(context.NhomNhanViens, nv => nv.IdNhomNhanVien, nnv => nnv.IdNhomNhanVien, (nv, nnv) => new { NhanVien = nv, NhomNhanVien = nnv })
                      .Select(x => x.NhanVien);
      list.phongs = context.Phongs
                  .Where(p => p.LoaiPhong.TenLoaiPhong == "Phòng siêu âm" && p.TinhTrang != "Tạm ngưng")
                  .ToList();
      list.benhNhans = context.BenhNhans.Where(p => p.NhomBenhNhan.TenNhom == "Mẹ bầu").ToList();
      list.thongTinThaiKies = context.ThongTinThaiKies.ToList();

      return View(list);
    }
    [HttpPost]
    public ActionResult TaoPhieuKham(PhieuKham pk)
    {
      pk.NgayTao = DateTime.Now;
      context.PhieuKhams.InsertOnSubmit(pk);
      context.SubmitChanges();
      return RedirectToAction("PhieuKham");
    }
    [HttpGet]
    public ActionResult SuaDoiPhieuKham(int id)
    {
      var list = new MutipleData2();
      list.phieuKhams = context.PhieuKhams.Where(p => p.IdSoPhieu == id).ToList();
      list.nhanViens = context.NhanViens.ToList();
      list.phongs = context.Phongs
                  .Where(p => p.LoaiPhong.TenLoaiPhong == "Phòng siêu âm" && p.TinhTrang != "Tạm ngưng")
                  .ToList();
      list.benhNhans = context.BenhNhans.Where(p => p.NhomBenhNhan.TenNhom == "Mẹ bầu").ToList();
      list.thongTinThaiKies = context.ThongTinThaiKies.ToList();
      return View(list);
    }
    [HttpPost]
    public ActionResult SuaDoiPhieuKham(PhieuKham pk)
    {
      PhieuKham phieu = context.PhieuKhams.FirstOrDefault(x => x.IdSoPhieu == pk.IdSoPhieu);
      phieu.IdNhanVien = pk.IdNhanVien;
      phieu.IdPhong = pk.IdPhong;
      phieu.IdBenhNhan = pk.IdBenhNhan;
      phieu.IdThongTinThaiKy = pk.IdThongTinThaiKy;
      context.SubmitChanges();

      return RedirectToAction("PhieuKham");
    }
    [HttpPost]
    public ActionResult XoaPhieuKham(int id)
    {
      PhieuKham phieu = context.PhieuKhams.FirstOrDefault(x => x.IdSoPhieu == id);
      context.PhieuKhams.DeleteOnSubmit(phieu);
      context.SubmitChanges();
      return RedirectToAction("PhieuKham");
    }
    [HttpGet]
    public ActionResult ChiTietPhieuKham(int id)
    {
      var list = new MutipleData2();
      list.phieuKhams = context.PhieuKhams.Where(p => p.IdSoPhieu == id).ToList();
      list.chiTietKhams = context.ChiTietKhams.ToList();
      list.nhanViens = context.NhanViens.ToList();
      list.phongs = context.Phongs.ToList();
      list.benhNhans = context.BenhNhans.Join(
                          context.GiaDinhs,
                          bn => bn.IdGiaDinh,
                          gd => gd.IdGiaDinh,
                          (bn, gd) => new {
                            BenhNhan = bn,
                            GiaDinh = gd
                          })
                      .Select(x => x.BenhNhan)
                      .ToList();
      list.thongTinThaiKies = context.ThongTinThaiKies.ToList();

      return View(list);
    }

    public ActionResult TaoChiTietPhieuKham()
    {
      var list = new MutipleData2();
      //list.chiTietKhams = db.ChiTietKhams.Include("PhieuKham").ToList();
      list.chiTietKhams = context.ChiTietKhams.ToList();

      return View(list);
    }
    [HttpPost]
    public ActionResult TaoChiTietPhieuKham(ChiTietKham ctk)
    {
      context.ChiTietKhams.InsertOnSubmit(ctk);
      context.SubmitChanges();


      return RedirectToAction("PhieuKham");
    }
    public ActionResult SuaDoiChiTietPhieuKham(int id)
    {
      var list = new MutipleData2();
      list.chiTietKhams = context.ChiTietKhams.Where(p => p.IdChiTietKham == id).ToList();
      return View(list);
    }
    [HttpPost]
    public ActionResult SuaDoiChiTietPhieuKham(ChiTietKham ctk)
    {
      ChiTietKham chitiet = context.ChiTietKhams.FirstOrDefault(x => x.IdChiTietKham == ctk.IdChiTietKham);
      chitiet.IdChiTietKham = ctk.IdChiTietKham;
      chitiet.IdSoPhieu = ctk.IdSoPhieu;
      chitiet.ChuanDoan = ctk.ChuanDoan;
      chitiet.PhuongPhap = ctk.PhuongPhap;
      chitiet.SoLan = ctk.SoLan;
      chitiet.GhiChu = ctk.GhiChu;
      context.SubmitChanges();
      return RedirectToAction("PhieuKham");
    }

    [HttpPost]
    public ActionResult XoaChiTietPhieuKham(int id)
    {
      ChiTietKham ctk = context.ChiTietKhams.FirstOrDefault(x => x.IdChiTietKham == id);
      context.ChiTietKhams.DeleteOnSubmit(ctk);
      context.SubmitChanges();
      return RedirectToAction("PhieuKham");
    }
  }
}
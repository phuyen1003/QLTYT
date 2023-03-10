using QLTYT.Models;
using QLTYT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
    public class TaoPhieuTiemChungController : Controller
    {
    // GET: TaoPhieuTiemChung
    private QLTYTDataContext context = new QLTYTDataContext();
    public ActionResult PhieuTiemChung()
    {
      var list = new MutipleData2();
      list.phieuTiemChungs = context.PhieuTiemChungs
                                .Join(context.NhanViens, ptc => ptc.IdNhanVien, nv => nv.IdNhanVien, (ptc, nv) => new { PhieuTiemChung = ptc, NhanVien = nv })
                                .Join(context.Phongs, ptcl => ptcl.PhieuTiemChung.IdPhong, phong => phong.IdPhong, (ptcl, phong) => new { ptcl.PhieuTiemChung, ptcl.NhanVien, Phong = phong })
                                .Join(context.BenhNhans, ptclp => ptclp.PhieuTiemChung.IdBenhNhan, bn => bn.IdBenhNhan, (ptclp, bn) => new { ptclp.PhieuTiemChung, ptclp.NhanVien, ptclp.Phong, BenhNhan = bn })
                                .Select(x => x.PhieuTiemChung);
      list.nhanViens = context.NhanViens
                      .Join(context.NhomNhanViens, nv => nv.IdNhomNhanVien, nnv => nnv.IdNhomNhanVien, (nv, nnv) => new { NhanVien = nv, NhomNhanVien = nnv })
                      .Select(x => x.NhanVien);
      list.phongs = from phong in context.Phongs
                    join loaiPhong in context.LoaiPhongs on phong.IdLoaiPhong equals loaiPhong.IdLoaiPhong
                    select phong;
      list.benhNhans = context.BenhNhans
                .Join(context.NhomBenhNhans, bn => bn.IdNhomBenhNhan, nb => nb.IdNhomBenhNhan, (bn, nb) => new { BenhNhan = bn, NhomBenhNhan = nb })
                .Select(x => x.BenhNhan);
      return View(list);
    }
    public ActionResult TaoPhieuTiemChung()
    {
      var list = new MutipleData2();
      list.phieuTiemChungs = context.PhieuTiemChungs
                                .Join(context.NhanViens, ptc => ptc.IdNhanVien, nv => nv.IdNhanVien, (ptc, nv) => new { PhieuTiemChung = ptc, NhanVien = nv })
                                .Join(context.Phongs, ptcl => ptcl.PhieuTiemChung.IdPhong, phong => phong.IdPhong, (ptcl, phong) => new { ptcl.PhieuTiemChung, ptcl.NhanVien, Phong = phong })
                                .Join(context.BenhNhans, ptclp => ptclp.PhieuTiemChung.IdBenhNhan, bn => bn.IdBenhNhan, (ptclp, bn) => new { ptclp.PhieuTiemChung, ptclp.NhanVien, ptclp.Phong, BenhNhan = bn })
                                .Select(x => x.PhieuTiemChung);
      list.nhanViens = context.NhanViens
                      .Join(context.NhomNhanViens, nv => nv.IdNhomNhanVien, nnv => nnv.IdNhomNhanVien, (nv, nnv) => new { NhanVien = nv, NhomNhanVien = nnv })
                      .Select(x => x.NhanVien);
      list.phongs = context.Phongs
                  .Where(p => p.LoaiPhong.TenLoaiPhong == "Phòng tiêm chủng" && p.TinhTrang != "Tạm ngưng")
                  .ToList();
      list.benhNhans = context.BenhNhans
                .Join(context.NhomBenhNhans, bn => bn.IdNhomBenhNhan, nb => nb.IdNhomBenhNhan, (bn, nb) => new { BenhNhan = bn, NhomBenhNhan = nb })
                .Select(x => x.BenhNhan);
      return View(list);
    }
    [HttpPost]
    public ActionResult TaoPhieuTiemChung(PhieuTiemChung ptc)
    {
      ptc.NgayTao = DateTime.Now;
      context.PhieuTiemChungs.InsertOnSubmit(ptc);
      context.SubmitChanges();
      return RedirectToAction("PhieuTiemChung");
    }
    [HttpGet]
    public ActionResult SuaDoiPhieuTiemChung(int id)
    {
      var list = new MutipleData2();
      list.phieuTiemChungs = context.PhieuTiemChungs.Where(p => p.IdSoPhieu == id).ToList();
      list.nhanViens = context.NhanViens.ToList();
      list.phongs = context.Phongs
                  .Where(p => p.LoaiPhong.TenLoaiPhong == "Phòng tiêm chủng" && p.TinhTrang != "Tạm ngưng")
                  .ToList();
      list.benhNhans = context.BenhNhans.Where(p => p.NhomBenhNhan.TenNhom == "Trẻ em").ToList();
      return View(list);
    }
    [HttpPost]
    public ActionResult SuaDoiPhieuTiemChung(PhieuTiemChung ptc)
    {
      PhieuTiemChung phieu = context.PhieuTiemChungs.FirstOrDefault(x => x.IdSoPhieu == ptc.IdSoPhieu);
      phieu.IdNhanVien = ptc.IdNhanVien;
      phieu.IdPhong = ptc.IdPhong;
      phieu.IdBenhNhan = ptc.IdBenhNhan;  
      context.SubmitChanges();
      return RedirectToAction("PhieuTiemChung");
    }
    [HttpPost]
    public ActionResult XoaPhieuTiemChung(int id)
    {
      PhieuTiemChung ptc = context.PhieuTiemChungs.FirstOrDefault(x => x.IdSoPhieu == id);
      context.PhieuTiemChungs.DeleteOnSubmit(ptc);
      context.SubmitChanges();
      return RedirectToAction("PhieuTiemChung");
    }
  }
}
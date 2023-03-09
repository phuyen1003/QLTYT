using QLTYT.Model2;
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
        private QuanLyTramYTeEntities1 db = new QuanLyTramYTeEntities1();
        // GET: TaoPhieuTiemChung
        public ActionResult PhieuTiemChung()
        {
            var list = new MutipleData2();
            list.phieuTiemChungs = db.PhieuTiemChungs.Include("NhanVien").Include("Phong").Include("BenhNhan");
            list.nhanViens = db.NhanViens.Include("NhomNhanVien").ToList();
            list.phongs = db.Phongs.Include("Loaiphong").ToList();
            list.benhNhans = db.BenhNhans.Include("NhomBenhNhan").ToList();
            return View(list);
        }
        public ActionResult TaoPhieuTiemChung()
        {
            var list = new MutipleData2();
            list.phieuTiemChungs = db.PhieuTiemChungs.Include("NhanVien").Include("Phong").Include("BenhNhan").ToList();
            list.nhanViens = db.NhanViens.Include("NhomNhanVien").ToList();
            list.phongs = db.Phongs
                        .Where(p => p.LoaiPhong.TenLoaiPhong == "Phòng tiêm chủng" && p.TinhTrang != "Tạm ngưng")
                        .ToList();
            list.benhNhans = db.BenhNhans.Where(p => p.NhomBenhNhan.TenNhom == "Trẻ em").ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult TaoPhieuTiemChung(PhieuTiemChung ptc)
        {
            ptc.NgayTao = DateTime.Now;
            db.PhieuTiemChungs.Add(ptc);
            db.SaveChanges();
            return RedirectToAction("PhieuTiemChung");
        }
        [HttpGet]
        public ActionResult SuaDoiPhieuTiemChung(int id)
        {
            var list = new MutipleData2();
            list.phieuTiemChungs = db.PhieuTiemChungs.Where(p => p.IdSoPhieu == id).ToList();
            list.nhanViens = db.NhanViens.ToList();
            list.phongs = db.Phongs
                        .Where(p => p.LoaiPhong.TenLoaiPhong == "Phòng tiêm chủng" && p.TinhTrang != "Tạm ngưng")
                        .ToList();
            list.benhNhans = db.BenhNhans.Where(p => p.NhomBenhNhan.TenNhom == "Trẻ em").ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult SuaDoiPhieuTiemChung(PhieuTiemChung ptc)
        {
            ptc.NgayTao = DateTime.Now;
            db.PhieuTiemChungs.Add(ptc);
            db.Entry(ptc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("PhieuTiemChung");
        }
        [HttpPost]
        public ActionResult XoaPhieuTiemChung(int id)
        {
            PhieuTiemChung ptc = db.PhieuTiemChungs.Find(id);
            db.PhieuTiemChungs.Remove(ptc);
            db.SaveChanges();
            return RedirectToAction("PhieuTiemChung");
        }
    }
}
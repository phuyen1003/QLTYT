using QLTYT.Model2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLTYT.ViewModel;

namespace QLTYT.Controllers
{
    public class TaoPhieuKhamController : Controller
    {
        private QuanLyTramYTeEntities1 db=new QuanLyTramYTeEntities1();
        // GET: TaoPhieu
        public ActionResult PhieuKham()
        {
            var list = new MutipleData2();
            list.phieuKhams = db.PhieuKhams.Include("NhanVien").Include("Phong").Include("BenhNhan").Include("ThongTinThaiKy").ToList();
            list.nhanViens = db.NhanViens.Include("NhomNhanVien").ToList() ;
            list.phongs=db.Phongs.Include("Loaiphong").ToList();
            list.benhNhans=db.BenhNhans.Include("NhomBenhNhan").ToList();
            list.thongTinThaiKies=db.ThongTinThaiKies.ToList();
            return View(list);
        }
        public ActionResult TaoPhieuKham()
        {            
            var list = new MutipleData2();
            list.phieuKhams = db.PhieuKhams.Include("NhanVien").Include("Phong").Include("BenhNhan").Include("ThongTinThaiKy").ToList();
            list.nhanViens = db.NhanViens.Include("NhomNhanVien").ToList();
            list.phongs = db.Phongs
                        .Where(p => p.LoaiPhong.TenLoaiPhong == "Phòng siêu âm" && p.TinhTrang != "Tạm ngưng")
                        .ToList();
            list.benhNhans = db.BenhNhans.Where(p => p.NhomBenhNhan.TenNhom == "Mẹ bầu").ToList();
            list.thongTinThaiKies = db.ThongTinThaiKies.ToList();
            
            return View(list);
        }
        [HttpPost]
        public ActionResult TaoPhieuKham(PhieuKham pk)
        {          
            pk.NgayTao = DateTime.Now;
            db.PhieuKhams.Add(pk);
            db.SaveChanges();
            return RedirectToAction("PhieuKham");
        }
        [HttpGet]
        public ActionResult SuaDoiPhieuKham(int id)
        {
            var list = new MutipleData2();
            list.phieuKhams=db.PhieuKhams.Where(p=>p.IdSoPhieu==id).ToList();
            list.nhanViens = db.NhanViens.ToList();
            list.phongs = db.Phongs
                        .Where(p => p.LoaiPhong.TenLoaiPhong == "Phòng siêu âm" && p.TinhTrang != "Tạm ngưng")
                        .ToList();
            list.benhNhans = db.BenhNhans.Where(p => p.NhomBenhNhan.TenNhom == "Mẹ bầu").ToList();
            list.thongTinThaiKies = db.ThongTinThaiKies.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult SuaDoiPhieuKham(PhieuKham pk)
        {
            pk.NgayTao = DateTime.Now;
            db.PhieuKhams.Add(pk);
            db.Entry(pk).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("PhieuKham");
        }
        [HttpPost]
        public ActionResult XoaPhieuKham(int id)
        {
            PhieuKham pk = db.PhieuKhams.Find(id);
            db.PhieuKhams.Remove(pk);
            db.SaveChanges();
            return RedirectToAction("PhieuKham");
        }
    }
}
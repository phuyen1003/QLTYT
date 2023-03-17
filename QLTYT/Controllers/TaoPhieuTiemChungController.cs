using QLTYT.Models;
using QLTYT.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
    [HttpGet]
    public ActionResult ChiTietPhieuTiemChung(int id)
    {
      var list = new MutipleData2();
      ViewBag.CTPTC = id;

      list.phieuTiemChungs = (
          from pt in context.PhieuTiemChungs
          join nv in context.NhanViens on pt.IdNhanVien equals nv.IdNhanVien
          join ph in context.Phongs on pt.IdPhong equals ph.IdPhong
          join bn in context.BenhNhans on pt.IdBenhNhan equals bn.IdBenhNhan
          where pt.IdSoPhieu == id
          select pt
      ).ToList();

      list.nhanViens = context.NhanViens.ToList();


      list.phongs = (
          from ph in context.Phongs
          join lp in context.LoaiPhongs on ph.IdPhong equals lp.IdLoaiPhong
          where lp.TenLoaiPhong == "Phòng tiêm chủng" && ph.TinhTrang != "Tạm ngưng"
          select ph
      ).ToList();

      list.benhNhans = context.BenhNhans.Where(p => p.NhomBenhNhan.TenNhom == "Trẻ em").ToList();


      list.chiTietTiemChungs = (
          from cttc in context.ChiTietTiemChungs
          join pu in context.PhanUngs on cttc.IdPhanUng equals pu.IdPhanUng
          join vx in context.VacXins on cttc.IdVacXin equals vx.IdVacXin
          where cttc.IdSoPhieu == id
          select cttc
      ).ToList();


      list.chiTietTrieuChungs = (
          from cttc in context.ChiTietTrieuChungs
          join tc in context.TrieuChungs on cttc.IdTrieuChung equals tc.IdTrieuChung
          join pu in context.PhanUngs on cttc.IdPhanUng equals pu.IdPhanUng
          select cttc
      ).ToList();

      return View(list);
    }

    public ActionResult TaoChiTietPhieuTiemChung(int id)
    {
      var list = new MutipleData2();
      ViewBag.CTPTC = id;
      // Lấy danh sách chi tiết tiêm chủng kèm theo thông tin liên quan
      var chiTietTiemChungs = (from cttc in context.ChiTietTiemChungs
                               join ptc in context.PhieuTiemChungs on cttc.IdSoPhieu equals ptc.IdSoPhieu
                               join pu in context.PhanUngs on cttc.IdPhanUng equals pu.IdPhanUng
                               join vx in context.VacXins on cttc.IdVacXin equals vx.IdVacXin
                               select new
                               {
                                 ChiTietTiemChung = cttc,
                                 PhieuTiemChung = ptc,
                                 PhanUng = pu,
                                 VacXin = vx
                               }).ToList();

      // Chuyển danh sách kết quả sang đối tượng MutipleData2
      list.chiTietTiemChungs = chiTietTiemChungs.Select(x => x.ChiTietTiemChung).ToList();
      list.phieuTiemChungs = chiTietTiemChungs.Select(x => x.PhieuTiemChung).Distinct().ToList();
      list.phanUngs = chiTietTiemChungs.Select(x => x.PhanUng).Distinct().ToList();
      list.vacXins = chiTietTiemChungs.Select(x => x.VacXin).Distinct().ToList();

      return View(list);
    }

    [HttpPost]
    public ActionResult TaoChiTietPhieuTiemChung(ChiTietTiemChung cttc, HttpPostedFileBase HinhAnhUpload)
    {
      context.ChiTietTiemChungs.InsertOnSubmit(cttc);
      context.SubmitChanges();
      if (HinhAnhUpload != null && HinhAnhUpload.ContentLength > 0)
      {
        int id = int.Parse(context.ChiTietTiemChungs.ToList().Last().IdChiTietTiemChung.ToString());
        string _FileName = "";
        int index = HinhAnhUpload.FileName.IndexOf('.');
        _FileName = "kh" + id.ToString() + "." + HinhAnhUpload.FileName.Substring(index + 1);
        string _path = Path.Combine(Server.MapPath("~/image"), _FileName);
        HinhAnhUpload.SaveAs(_path);

        ChiTietTiemChung cttc1 = context.ChiTietTiemChungs.FirstOrDefault(x => x.IdChiTietTiemChung == id);
        cttc1.HinhAnh = _FileName;
        context.SubmitChanges();
      }

      return RedirectToAction("PhieuTiemChung");
    }
    public ActionResult SuaDoiChiTietPhieuTiemChung(int id)
    {
      var list = new MutipleData2();
      list.chiTietTiemChungs = context.ChiTietTiemChungs.Where(p => p.IdChiTietTiemChung == id).ToList();
      list.phanUngs = context.PhanUngs.ToList();
      list.vacXins = context.VacXins.ToList();
      return View(list);
    }
    [HttpPost]
    public ActionResult SuaDoiChiTietPhieuTiemChung(ChiTietTiemChung cttc, HttpPostedFileBase HinhAnhUpload)
    {
      ChiTietTiemChung chitiet = context.ChiTietTiemChungs.FirstOrDefault(x => x.IdChiTietTiemChung == cttc.IdChiTietTiemChung);
      string path = uploadimage(HinhAnhUpload);


      cttc.HinhAnh = path;

      if (ModelState.IsValid)
      {
        cttc.IdChiTietTiemChung = cttc.IdChiTietTiemChung;
        cttc.IdSoPhieu = cttc.IdSoPhieu;
        cttc.IdPhanUng = chitiet.IdPhanUng;
        cttc.IdVacXin = chitiet.IdVacXin;
        cttc.DiaDiem = chitiet.DiaDiem;
        cttc.GhiChu = chitiet.GhiChu;
        context.SubmitChanges();
        return RedirectToAction("PhieuTiemChung");
      }
      return View(cttc);
    }
    public string uploadimage(HttpPostedFileBase file)
    {
      Random r = new Random();
      string path = "-1";
      int random = r.Next();
      if (file != null && file.ContentLength > 0)
      {
        string extension = Path.GetExtension(file.FileName);
        if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
        {
          try
          {
            path = Path.Combine(Server.MapPath("~/image"), random + Path.GetFileName(file.FileName));
            file.SaveAs(path);
            path = "" + random + Path.GetFileName(file.FileName);
          }
          catch (Exception ex)
          {
            path = "-1";
          }
        }
        else
        {
          Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
        }
      }
      else
      {
        Response.Write("<script>alert('Please select a file'); </script>");
        path = "-1";
      }
      return path;
    }
    [HttpPost]
    public ActionResult XoaChiTietPhieuTiemChung(int id)
    {
      ChiTietTiemChung cttc = context.ChiTietTiemChungs.FirstOrDefault(x => x.IdChiTietTiemChung == id);
      context.ChiTietTiemChungs.DeleteOnSubmit(cttc);
      context.SubmitChanges();
      return RedirectToAction("PhieuTiemChung");
    }
  }
}
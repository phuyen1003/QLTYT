using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTYT.ViewModel
{
  public class MutipleData2
  {
    public IEnumerable<PhieuKham> phieuKhams { get; set; }
    public IEnumerable<PhieuTiemChung> phieuTiemChungs { get; set; }
    public IEnumerable<ChiTietTiemChung> chiTietTiemChungs { get; set; }
    public IEnumerable<ChiTietKham> chiTietKhams { get; set; }
    public IEnumerable<PhanUng> phanUngs { get; set; }
    public IEnumerable<ChiTietTrieuChung> chiTietTrieuChungs { get; set; }
    public IEnumerable<VacXin> vacXins { get; set; }
    public IEnumerable<TrieuChung> trieuChungs { get; set; }
    public IEnumerable<NhanVien> nhanViens { get; set; }
    public IEnumerable<NhomNhanVien> nhomNhanViens { get; set; }
    public IEnumerable<Phong> phongs { get; set; }
    public IEnumerable<LoaiPhong> loaiPhongs { get; set; }
    public IEnumerable<BenhNhan> benhNhans { get; set; }
    public IEnumerable<GiaDinh> giaDinhs { get; set; }
    public IEnumerable<NhomBenhNhan> nhomBenhNhans { get; set; }
    public IEnumerable<ThongTinThaiKy> thongTinThaiKies { get; set; }
  }
}
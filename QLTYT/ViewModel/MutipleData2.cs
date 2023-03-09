using QLTYT.Model2;
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
        public IEnumerable<NhanVien> nhanViens { get; set; }
        public IEnumerable<NhomNhanVien> nhomNhanViens { get; set; }
        public IEnumerable<Phong> phongs { get; set; }
        public IEnumerable<LoaiPhong> loaiPhongs { get; set;}
        public IEnumerable<BenhNhan> benhNhans { get; set; }
        public IEnumerable<NhomBenhNhan> nhomBenhNhans { get; set; }
        public IEnumerable<ThongTinThaiKy> thongTinThaiKies { get; set; }
    }
}
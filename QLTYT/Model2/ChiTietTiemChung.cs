//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLTYT.Model2
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietTiemChung
    {
        public int IdChiTietTiemChung { get; set; }
        public Nullable<int> IdSoPhieu { get; set; }
        public Nullable<int> IdChiTietTrieuChung { get; set; }
        public Nullable<int> IdVacXin { get; set; }
        public string SoLo { get; set; }
        public string DiaDiem { get; set; }
        public string GhiChu { get; set; }
        public string HinhAnh { get; set; }
    
        public virtual ChiTietTrieuChung ChiTietTrieuChung { get; set; }
        public virtual PhieuTiemChung PhieuTiemChung { get; set; }
        public virtual VacXin VacXin { get; set; }
    }
}

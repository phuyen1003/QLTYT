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
    
    public partial class VacXin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VacXin()
        {
            this.ChiTietTiemChungs = new HashSet<ChiTietTiemChung>();
            this.LichTiemChungs = new HashSet<LichTiemChung>();
        }
    
        public int IdVacXin { get; set; }
        public Nullable<int> IdBenh { get; set; }
        public string TenVacXin { get; set; }
        public Nullable<double> LieuLuong { get; set; }
        public string DonVi { get; set; }
        public Nullable<double> DonGia { get; set; }
        public string SoLo { get; set; }
    
        public virtual Benh Benh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietTiemChung> ChiTietTiemChungs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichTiemChung> LichTiemChungs { get; set; }
    }
}

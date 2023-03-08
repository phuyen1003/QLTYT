using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
    public class BenhNhanController : Controller
    {
        // GET: Benh
       
        public ActionResult Index()
        {
            QLTYTDataContext context = new QLTYTDataContext();
            List <SP_ALLBNResult> bn = context.SP_ALLBN().ToList();
            return View(bn);
        }
        public ActionResult Create()
        {
            QLTYTDataContext context = new QLTYTDataContext();

            List<NhomBenhNhan> listnhombenhnhan = context.NhomBenhNhans.ToList();
            List<GiaDinh> listgiadinh = context.GiaDinhs.ToList();
            

            ViewBag.nhombenhnhan = listnhombenhnhan;
            ViewBag.giadinh = listgiadinh;
           
            if (Request.Form.Count > 0)
            {
               
                String hoten = Request.Form["HoTen"];
                String cccd = Request.Form["CCCD"];
                String bhyt = Request.Form["BHYT"];
                int gioitinh = int.Parse(Request.Form["GioiTinh"]);
                String sdt = Request.Form["SDT"];
                String email = Request.Form["Email"];
                String diachi = Request.Form["DiaChi"];
                float chieucao = float.Parse(Request.Form["ChieuCao"]);
                float cannang = float.Parse(Request.Form["CanNang"]);
                int idnbn = int.Parse(Request.Form["IdNhomBenhNhan"]);
                int idgd = int.Parse(Request.Form["IdGiaDinh"]);


                BenhNhan sp = new BenhNhan();

                sp.HoTen = hoten;
                sp.CCCD = cccd;
                sp.BHYT = bhyt;
                sp.GioiTinh = gioitinh;
                sp.SDT= sdt;
                sp.Email = email;
                sp.ChieuCao= chieucao;
                sp.CanNang = cannang;
                sp.IdNhomBenhNhan = idnbn;
                sp.IdGiaDinh = idgd;


               
                context.BenhNhans.InsertOnSubmit(sp);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }


            return View();
        }

        public ActionResult Edit(int id)

        {

          
            QLTYTDataContext context = new QLTYTDataContext();

          
            BenhNhan sp = context.BenhNhans.FirstOrDefault(x => x.IdBenhNhan == id);

            List<NhomBenhNhan> listnhombenhnhan = context.NhomBenhNhans.ToList();
            List<GiaDinh> listgiadinh = context.GiaDinhs.ToList();

            ViewBag.nhombenhnhan = listnhombenhnhan;
            ViewBag.giadinh = listgiadinh;


            if (Request.Form.Count > 0)
            {
                String hoten = Request.Form["HoTen"];
                String cccd = Request.Form["CCCD"];
                String bhyt = Request.Form["BHYT"];
                int gioitinh = int.Parse(Request.Form["GioiTinh"]);
                String sdt = Request.Form["SDT"];
                String email = Request.Form["Email"];
                String diachi = Request.Form["DiaChi"];
                float chieucao = float.Parse(Request.Form["ChieuCao"]);
                float cannang = float.Parse(Request.Form["CanNang"]);
                int idnbn = int.Parse(Request.Form["IdNhomBenhNhan"]);
                int idgd = int.Parse(Request.Form["IdGiaDinh"]);



                sp.HoTen = hoten;
                sp.CCCD = cccd;
                sp.BHYT = bhyt;
                sp.GioiTinh = gioitinh;
                sp.SDT = sdt;
                sp.Email = email;
                sp.ChieuCao = chieucao;
                sp.CanNang = cannang;
                sp.IdNhomBenhNhan = idnbn;
                sp.IdGiaDinh = idgd;




                
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(sp);
        }

        public ActionResult Delete(int id)
        {
            QLTYTDataContext context = new QLTYTDataContext();
            BenhNhan sp = context.BenhNhans.FirstOrDefault(x => x.IdBenhNhan == id);
            if (sp != null)
            {
               
                context.BenhNhans.DeleteOnSubmit(sp);
                context.SubmitChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
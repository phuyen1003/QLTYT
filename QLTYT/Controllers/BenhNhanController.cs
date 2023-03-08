using OfficeOpenXml;
using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.IO;
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


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);

                using (var package = new ExcelPackage(new FileInfo(path)))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var workbook = package.Workbook;
                    var worksheet = workbook.Worksheets[0];

                    for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        int idbn = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                        int idnbn = int.Parse(worksheet.Cells[row, 2].Value.ToString());
                        int idgd = int.Parse(worksheet.Cells[row, 3].Value.ToString());
                        string name = worksheet.Cells[row, 4].Value.ToString();
                        string cccd = worksheet.Cells[row, 5].Value.ToString();
                        string bhyt = worksheet.Cells[row, 6].Value.ToString();
                        int gioitinh = int.Parse(worksheet.Cells[row, 7].Value.ToString());
                        DateTime ngaysinh = DateTime.Parse(worksheet.Cells[row, 8].Value.ToString());
                        string sdt = worksheet.Cells[row, 9].Value.ToString();
                        string email = worksheet.Cells[row, 10].Value.ToString();
                        string diachi = worksheet.Cells[row, 11].Value.ToString();
                        float chieucao = float.Parse(worksheet.Cells[row, 12].Value.ToString());
                        float cannang = float.Parse(worksheet.Cells[row, 13].Value.ToString());


                        using (var context = new QLTYTDataContext())
                        {
                            var user = new BenhNhan { IdBenhNhan = idbn, IdNhomBenhNhan = idnbn,IdGiaDinh=idgd,HoTen=name,CCCD=cccd,BHYT=bhyt
                                ,GioiTinh=gioitinh,NgaySinh=ngaysinh,SDT=sdt, Email = email,DiaChi=diachi,ChieuCao=chieucao,CanNang=cannang };
                            context.BenhNhans.InsertOnSubmit(user);
                            context.SubmitChanges();
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }

    }
}
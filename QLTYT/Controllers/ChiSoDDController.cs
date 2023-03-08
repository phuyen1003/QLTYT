using ClosedXML.Excel;
using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
    public class ChiSoDDController : Controller
    {
        // GET: ChiSoDD
        public ActionResult Index()
        {
            QLTYTDataContext context = new QLTYTDataContext();
            List<SP_BMIResult> bmi = context.SP_BMI().ToList();
            return View(bmi);
        }
        public ActionResult ExportToExcel()
        {
            QLTYTDataContext context = new QLTYTDataContext();

            List<SP_BMIResult> bmi = context.SP_BMI().ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("CHỈ SỐ BMI");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Họ & Tên";
                worksheet.Cell(currentRow, 3).Value = "Tuổi";
                worksheet.Cell(currentRow, 4).Value = "BMI";
                worksheet.Cell(currentRow, 5).Value = "Cảnh báo";
               


                foreach (var nv in bmi)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = nv.IdBenhNhan;
                    worksheet.Cell(currentRow, 2).Value = nv.HoTen;
                    worksheet.Cell(currentRow, 3).Value = nv.Tuổi;
                    worksheet.Cell(currentRow, 4).Value = nv.BMI_của_trẻ_em;
                    worksheet.Cell(currentRow, 5).Value = nv.CanhBao;

                    



                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BMI.xlsx");
                }
            }
        }

    }
}
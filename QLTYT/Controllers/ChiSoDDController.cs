using ClosedXML.Excel;
using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace QLTYT.Controllers
{
  [Authorize]
  public class ChiSoDDController : Controller
  {
    // GET: ChiSoDD
    QLTYTDataContext context = new QLTYTDataContext();
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
    public static class MailHelper
    {
      public static void Send(MailMessage message)
      {
        using (var client = new SmtpClient())
        {
          client.Send(message);
        }
      }
    }

    public ActionResult SendEmail()
    {
      // Lấy dữ liệu từ stored procedure
      var data = context.SP_CanhBao().ToList();

      // Lấy địa chỉ email của gia đình
      var email = data.FirstOrDefault()?.Email;

      if (string.IsNullOrEmpty(email))
      {
        return Content("Không tìm thấy địa chỉ email cho gia đình này.");
      }

      try
      {
        // Cấu hình thông tin email
        var fromEmail = new MailAddress("ltby1004@gmail.com", "TRẠM Y TẾ");
        var toEmail = new MailAddress(email);
        var password = "qbgxljiobhacdwhw";

        // Tạo message
        var subject = "Cảnh báo dinh dưỡng";
        var body = "Đây là email cảnh báo dinh dưỡng.";
        var message = new MailMessage(fromEmail, toEmail);
        message.Subject = subject;
        message.Body = body;

        // Thiết lập SMTP client
        var smtpClient = new SmtpClient();
        smtpClient.Host = "smtp.gmail.com";
        smtpClient.Port = 587;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(fromEmail.Address, password);
        smtpClient.EnableSsl = true;

        // Gửi email
        smtpClient.Send(message);

        return Content("Gửi email thành công.");
      }
      catch (Exception ex)
      {
        return Content($"Gửi email thất bại: {ex.Message}");
      }
    }


  }
}
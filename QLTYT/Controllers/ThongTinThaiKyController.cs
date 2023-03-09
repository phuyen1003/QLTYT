using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
    public class ThongTinThaiKyController : Controller
    {
        // GET: ThongTinThaiKy
        QLTYTDataContext context = new QLTYTDataContext();
        public ActionResult Index()
        {
            QLTYTDataContext context = new QLTYTDataContext();
            List<SP_thaikyResult> tk = context.SP_thaiky().ToList();
            return View(tk);
        }
        



    }
}
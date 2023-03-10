using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
    public class GiaDinhController : Controller
    {
    [Authorize]
    // GET: GiaDinh
    public ActionResult Index()
        {
            QLTYTDataContext context = new QLTYTDataContext();
            List<SP_QHGDResult> gd = context.SP_QHGD().ToList();
            return View(gd);
        }
    }
}
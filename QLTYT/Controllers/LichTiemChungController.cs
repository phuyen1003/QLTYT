using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTYT.Controllers
{
  public class LichTiemChungController : Controller
  {
    // GET: LichTiemChung
    private QLTYTDataContext context = new QLTYTDataContext();

    public ActionResult Index()
    {
      List<LichTiemChung> list = context.LichTiemChungs.ToList();
      return View(list);
    }
  }
}
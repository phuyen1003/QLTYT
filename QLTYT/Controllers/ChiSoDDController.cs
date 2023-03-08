using QLTYT.Models;
using System;
using System.Collections.Generic;
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

        
    }
}
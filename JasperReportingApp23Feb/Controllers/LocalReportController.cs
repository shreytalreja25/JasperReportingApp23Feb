using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JasperReportingApp23Feb.Controllers
{
    public class LocalReportController : Controller
    {
        // GET: LocalReport
        public ActionResult ViewReport(string reportName)
        {
            string reportPath = Server.MapPath("~/Reports/" + reportName + ".jrxml");

            if (!System.IO.File.Exists(reportPath))
            {
                return HttpNotFound();
            }

            ViewBag.ReportPath = reportPath;

            return View();
        }
    }
}
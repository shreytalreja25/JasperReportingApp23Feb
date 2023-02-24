using JasperServer.Client.Core;
using JasperServer.Client;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using JasperServer.Client.Core;

namespace JasperReportingApp23Feb.Controllers
{
    public class ReportController : Controller
    {   
        
        // GET: Report
        public ActionResult Index()
        {
            // Set the server URL, username, and password

            var serverUrl = "http://localhost:8081/jasperserver";
            var username = "jasperadmin";
            var password = "jasperadmin";
            
            // Create a new instance of the JasperserverRestClient
            var client = new JasperserverRestClient(serverUrl, username, password);
            
            // Retrieve the list of reports
            var reports = client.GetReport("/Reports/Interactive");
            
            // Create a SelectList of report names and paths
            var reportSelectList = new SelectList(reports.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Path
            }), "Value", "Text");

            // Pass the SelectList to the view
            ViewBag.ReportSelectList = reportSelectList;
            return View();
        }

        [HttpPost]
        public ActionResult ViewReport(string reportPath)
        {
            var serverUrl = "http://localhost:8081/jasperserver";
            var username = "jasperadmin";
            var password = "jasperadmin";
            var client = new JasperserverRestClient(serverUrl, username, password);

            var reportUrl = client.ViewReport(reportUri);
            ViewBag.ReportUrl = reportUrl;

            return View(reportPath);
        }
        
        [HttpPost]
        public FileResult DownloadReport(string reportUri)
        {
            var serverUrl = "http://localhost:8081/jasperserver";
            var username = "jasperadmin";
            var password = "jasperadmin";
            var client = new JasperserverRestClient(serverUrl, username, password);

            var reportBytes = client.DownloadReport(reportUri, "PDF");

            return File(reportBytes, "application/PDF", "report.pdf");
        }

        public ActionResult GetReport(string reportPath)
        {
            var serverUrl = "http://localhost:8081/jasperserver";
            var username = "jasperadmin";
            var password = "jasperadmin";
            var client = new JasperserverRestClient(serverUrl, username, password);

            var reportUrl = client.GenerateReport(reportPath);

            return Content(reportUrl);
        }
    }
}
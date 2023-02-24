using JasperServer.Client.Core;
using System.Collections.Generic;
using System.Web.Mvc;

namespace YourNamespace.Controllers
{
    public class JasperServerController : Controller
    {
        private readonly string serverUrl = "http://localhost:8081/jasperserver";
        private readonly string username = "jasperadmin";
        private readonly string password = "jasperadmin";

        public ActionResult Index()
        {
            var client = new JasperserverRestClient(serverUrl, username, password);
            var reports = new List<Report>();

            // change "/Reports/Interactive" to the path of your reports folder
            var reportList = client.SearchRepository("/Reports/Interactive");

            foreach (var report in reportList)
            {
                reports.Add(new Report { Uri = report.Uri, Label = report.Label });
            }

            return View(reports);
        }

        public ActionResult ViewReport(string reportUri)
        {
            var client = new JasperserverRestClient(serverUrl, username, password);
            var parameters = new Dictionary<string, object>();
            var reportData = client.RunReport(reportUri, "pdf", parameters);
            return File(reportData, "application/pdf", "Report.pdf");
        }

        public ActionResult DownloadReport(string reportUri)
        {
            var client = new JasperserverRestClient(serverUrl, username, password);
            var parameters = new Dictionary<string, object>();
            var reportData = client.SaveToFile(reportUri, "pdf", parameters);

            var fileName = reportUri.Substring(reportUri.LastIndexOf("/") + 1);
            return File(reportData, "application/pdf", fileName + ".pdf");
        }
    }
}

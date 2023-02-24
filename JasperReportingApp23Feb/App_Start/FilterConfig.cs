using System.Web;
using System.Web.Mvc;

namespace JasperReportingApp23Feb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

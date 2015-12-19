using System.Web.Mvc;

namespace EWG.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return File(Server.MapPath("/") + "index.html", "text/html"); 
        }
    }
}

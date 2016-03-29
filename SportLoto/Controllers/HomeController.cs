using System.Web.Mvc;

namespace SportLoto.Controllers
{
    public class HomeController : ApplicationController
    {
        public HomeController() : base() { }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
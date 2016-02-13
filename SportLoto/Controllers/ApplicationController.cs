using SportLoto.Repositories;
using System.Web.Mvc;

namespace SportLoto.Controllers
{
    public class ApplicationController : Controller
    {
        protected ApplicationDbContext db;

        public ApplicationController()
        {
            db = new ApplicationDbContext();
        }
    }
}
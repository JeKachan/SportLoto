using Microsoft.AspNet.Identity;
using SportLoto.DbModels;
using SportLoto.Repositories;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportLoto.Controllers
{
    public class ApplicationController : Controller
    {
        private bool disposedValue = false; // To detect redundant calls
        protected ISqlRepository repository { get; set; }
        protected ApplicationUser CurrentUser { get; set; }

        public ApplicationController()
        {
            repository = new SqlRepository();
        }

        public ApplicationController(ISqlRepository _repository)
        {
            repository = _repository;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            string currentUserId = requestContext.HttpContext.User?.Identity.GetUserId();
            CurrentUser = repository.Users.FirstOrDefault(x => x.Id == currentUserId);

        }

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    CurrentUser = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                repository.Dispose();
                // TODO: set large fields to null.
                repository = null;

                disposedValue = true;
            }
            base.Dispose(disposing);
        }

        ~ApplicationController()
        {
            Dispose(false);
        }
    }
}
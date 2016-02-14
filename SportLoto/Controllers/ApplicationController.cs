using Microsoft.AspNet.Identity;
using SportLoto.DbModels;
using SportLoto.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportLoto.Controllers
{
    public class ApplicationController : Controller
    {
        protected ApplicationDbContext db;
        protected ApplicationUser currentUser;
        private bool disposedValue = false; // To detect redundant calls


        public ApplicationController()
        {
            db = new ApplicationDbContext();
           
        }

        protected override void Initialize(RequestContext requestContext)
        {
            string currentUserId = requestContext.HttpContext.User?.Identity.GetUserId();
            currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            base.Initialize(requestContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    currentUser = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                db.Dispose();
                // TODO: set large fields to null.
                db = null;

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
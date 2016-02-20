using Microsoft.AspNet.Identity;
using SportLoto.DbModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SportLoto.Models;
using System.Web.Routing;
using System.Linq;

namespace SportLoto.Controllers
{
    [Authorize]
    public class TicketController : ApplicationController
    {

        protected Drawing CurrentDrawing { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            CurrentDrawing = repository.GetLastDrawing();
        }
        
        // GET: Ticket
        public ActionResult Index()
        {
            var model = new IndexTicketViewModel();
            if(CurrentDrawing != null)
            {
                model.Tickets = (from t in CurrentDrawing.Tickets
                                 where t.ApplicationUserId == User.Identity.GetUserId()
                                 select t).ToList();

            }
            return View(model);
        }

        

        [HttpGet]
        public ActionResult CreateTicket()
        {
            var model = new CreateTicketViewModel();
            model.Drawing = CurrentDrawing;

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateTicket(string ticketJson)
        {
            var resultJson = new CreateTicketJson();
            if (CurrentDrawing != null)
            {
                var js = new JavaScriptSerializer();
                var ticket = js.Deserialize<List<List<int>>>(ticketJson);
                var newTicket = new Ticket()
                {
                    TicketNo = ticketJson,
                    Drawing = CurrentDrawing,
                    ApplicationUserId = User.Identity.GetUserId(),
                };
                resultJson.Succesed = await repository.CreateTicketAsync(newTicket);
                if (!resultJson.Succesed)
                {
                    resultJson.Errors.Add("Cant save ticket.");
                }
            }
            return Json(resultJson);
        }
    }
}
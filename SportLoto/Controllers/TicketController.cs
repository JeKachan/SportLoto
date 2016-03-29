using Microsoft.AspNet.Identity;
using SportLoto.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SportLoto.Models;
using System.Web.Routing;
using System.Linq;
using System;

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
        public async Task<ActionResult> Index()
        {
            var model = new IndexTicketViewModel();
            if(CurrentDrawing != null)
            {
                model.Tickets = await repository.GetNotPayedTicketsAsync(CurrentUser.Id);

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

        public ActionResult Success(SuccessViewModel model)
        {
            return Content("In development");
        }

        #region pay pal temp

        public async Task<ActionResult> PayTicket()
        {
            var tickets = await repository.GetNotPayedTicketsAsync(CurrentUser.Id);
            var ticketPrice = 20m;
            var transtaction = new Transaction()
            {
                Amount = ticketPrice,
                Quantity = tickets.Count,
                ItemTotal = ticketPrice * tickets.Count,
                ApplicationUserId = CurrentUser.Id,
                DrawingId = CurrentDrawing.Id,
                Confirmed = false,
            };
            var createTransactionResult = await repository.CreateTransactionAsync(transtaction);
            if (createTransactionResult)
            {
                return RedirectToAction("Success", new SuccessViewModel
                {
                    transaction_id = transtaction.Id,
                    first_name = CurrentUser.UserName,
                    last_name = CurrentUser.Surname,
                    payment_status = "Completed",
                    payer_email = CurrentUser.Email,
                    payment_gross = transtaction.ItemTotal,
                    mc_currency = "USD",
                    custom = $"transactioId={transtaction.Id}&ticketsId={String.Join(",", tickets.Select(x => x.Id))}"

                } );
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
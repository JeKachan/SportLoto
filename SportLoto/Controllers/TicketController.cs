﻿using Microsoft.AspNet.Identity;
using SportLoto.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SportLoto.Models;
using System.Web.Routing;
using System.Linq;
using System;
using System.Web;
using SportLoto.DbModels.Data;

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

        public async Task<ActionResult> Success(SuccessViewModel model)
        {
            var param = HttpUtility.ParseQueryString(model.custom);
            var ticketsId = param["ticketsId"].Split(',').Select(x => Int32.Parse(x)).ToList();
            var transaction = await repository.GetTransactionByIdAsync(model.transaction_id);
            if (transaction != null)
            {
                var tickets = await repository.GetTicketsByIdsAsync(ticketsId);
                if (ticketsId.Count > 0)
                {
                    transaction.Confirmed = true;
                    transaction.PaymentDate = DateTime.Now;

                    tickets.ForEach(x => 
                    {
                        x.TransactionId = transaction.Id;
                        x.DrawingId = transaction.DrawingId;
                    });
                    await repository.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }

        #region pay pal temp

        public async Task<ActionResult> PayTicket()
        {
            var tickets = await repository.GetNotPayedTicketsAsync(CurrentUser.Id);
            var ticketPrice = SportLotoSettings.TicketPrice;
            var transtaction = new Transaction()
            {
                Amount = ticketPrice,
                Quantity = tickets.Count,
                ItemTotal = ticketPrice * tickets.Count,
                ApplicationUserId = CurrentUser.Id,
                DrawingId = CurrentDrawing.Id,
                Confirmed = false,
            };
            //var createTransactionResult = await repository.CreateTransactionAsync(transtaction);
            //if (createTransactionResult)
            //{
            //    return RedirectToAction("Success", new SuccessViewModel
            //    {
            //        transaction_id = transtaction.Id,
            //        first_name = CurrentUser.UserName,
            //        last_name = CurrentUser.Surname,
            //        payment_status = "Completed",
            //        payer_email = CurrentUser.Email,
            //        payment_gross = transtaction.ItemTotal,
            //        mc_currency = "USD",
            //        custom = $"transactioId={transtaction.Id}&ticketsId={String.Join(",", tickets.Select(x => x.Id))}"
            //    });
            //}
            return RedirectToAction("Index");
        }
        #endregion

        public async Task<ActionResult> PurchasedTickets()
        {
            var model = new IndexTicketViewModel();
            if (CurrentDrawing != null)
            {
                model.Tickets = await repository.GetTicketsByUserDrawingIdsAsync(CurrentUser.Id, CurrentDrawing.Id);
            }
            return View("Index", model);
        }
    }
}
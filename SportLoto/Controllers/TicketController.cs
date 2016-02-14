using Microsoft.AspNet.Identity;
using SportLoto.DbModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SportLoto.Controllers
{
    [Authorize]
    public class TicketController : ApplicationController
    {
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateTicket()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateTicket(string ticketJson)
        {
            var js = new JavaScriptSerializer();
            var ticket = js.Deserialize<List<List<int>>>(ticketJson);
            var newTicket = new Ticket()
            {
                CreateDate = DateTime.Now,
                TicketNo = ticketJson,
                DrawingId = 1,
                ApplicationUserId = User?.Identity.GetUserId(),
            };
            db.Tickets.Add(newTicket);
            //db.SaveChanges();
            return Json(new { });
        }
    }
}
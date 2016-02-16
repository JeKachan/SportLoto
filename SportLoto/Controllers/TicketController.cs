using Microsoft.AspNet.Identity;
using SportLoto.DbModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<ActionResult> CreateTicket()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateTicket(string ticketJson)
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
            //bool result = await db.CreateTicketAsync(newTicket);
            return Json(new { });
        }
    }
}
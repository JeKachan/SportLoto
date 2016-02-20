using SportLoto.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SportLoto.Models
{

    public class IndexTicketViewModel
    {

        public IndexTicketViewModel()
        {
            Tickets = new List<Ticket>();
        }

        public List<Ticket> Tickets { get; set; }

        public List<List<int>> GetTicketNo(Ticket ticket)
        {
            var js = new JavaScriptSerializer();
            return js.Deserialize<List<List<int>>>(ticket.TicketNo);
        }
    }

    public class CreateTicketViewModel
    {
        public Drawing Drawing { get; set; }
    }

    public class CreateTicketJson
    {
        public bool Succesed { get; set; }

        public List<string> Errors { get; set; }
    }
}
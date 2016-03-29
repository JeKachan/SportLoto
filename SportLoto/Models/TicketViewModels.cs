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

    public class SuccessViewModel
    {
        public int transaction_id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string payment_status { get; set; }

        public string payer_email { get; set; }

        public decimal payment_gross { get; set; }

        public string mc_currency { get; set; }

        public string custom { get; set; }

    }
}
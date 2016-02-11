using System;
using System.ComponentModel.DataAnnotations;

namespace SportLoto.Entity
{
    //ID int primary key identity(1,1),
    //TicketNo nvarchar(30),
    //TicketDate datetime,
    //UserID int,
    //DrawingID int
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        [MaxLength(300)]
        public string TicketNo { get; set; }

        public string ApplicationUserID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}

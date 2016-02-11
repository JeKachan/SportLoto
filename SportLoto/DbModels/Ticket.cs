using SportLoto.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SportLoto.DbModels
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

        [Required]
        public DateTime CreateDate { get; set; }

        [MaxLength(300)]
        [Required]
        public string TicketNo { get; set; }

        [Required]
        public int DrawingId { get; set; }

        public virtual Drawing Drawing { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}

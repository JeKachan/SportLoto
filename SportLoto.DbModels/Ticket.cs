using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLoto.DbModels
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDate { get; set; }

        [MaxLength(300)]
        [Required]
        public string TicketNo { get; set; }

        public int? DrawingId { get; set; }
        public virtual Drawing Drawing { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? TransactionId { get; set; }

        public Transaction Transactions { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLoto.DbModels
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        //Item unit price.
        public decimal Amount { get; set; }

        [Required]
        //Item unit quantity.
        public int Quantity { get; set; }

        [Required]
        //Sum of costs of all items in this order.
        public decimal ItemTotal { get; set; }

        //Item description. 
        [MaxLength(300)]
        public string Description { get; set; }

        //Transaction confirmed
        public bool Confirmed { get; set; }

        [MaxLength(50)]
        public string PayPalTransactionId { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public int DrawingId { get; set; }

        public virtual Drawing Drawing { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDate { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}
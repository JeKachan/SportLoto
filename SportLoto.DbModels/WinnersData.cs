using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLoto.DbModels
{
    [Table("WinnersData")]
    public class WinnersData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public int DrawingId { get; set; }

        [Required]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        public bool PaymentMade { get; set; }

        public byte NumberMatchCount { get; set; }
    }
}

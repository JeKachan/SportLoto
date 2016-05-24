using System.ComponentModel.DataAnnotations;

namespace SportLoto.DbModels
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        public decimal TicketPrice { get; set; } // 5

        public byte DigitsNoInTicket { get; set; } // 6

        public decimal fourNoMatched { get; set; } // 0.3

        public decimal fiveNoMatched { get; set; } // 0.7

        public string Currency { get; set; } // "USD"

        public byte MaxCellsInSection { get; set; } // 6

        public byte SectionCount { get; set; } // 6

        public bool EnableJackpotWin { get; set; } //fasle

        public decimal JackpotPart { get; set; } // 0.4

        public decimal OwnerPart { get; set; } // 0.3

        public decimal WinnersPart { get; set; } // 0.3

        public decimal OwnerFond { get; set; } //10 000
    }
}

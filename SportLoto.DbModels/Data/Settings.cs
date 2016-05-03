namespace SportLoto.DbModels.Data
{
    public static class SportLotoSettings
    {
        public static decimal TicketPrice { get; } = 20;
        public static byte DigitsNoInTicket { get; } = 6;
        public static byte JackpotNoCount { get; } = 6;
        public static string Currency { get; } = "USD";
        public static byte MaxSellInSection { get; } = 6;
        public static byte SectionCount { get; } = 6;
    }
}

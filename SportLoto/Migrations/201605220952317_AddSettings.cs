namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DigitsNoInTicket = c.Byte(nullable: false),
                        fourNoMatched = c.Decimal(nullable: false, precision: 18, scale: 2),
                        fiveNoMatched = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                        MaxCellsInSection = c.Byte(nullable: false),
                        SectionCount = c.Byte(nullable: false),
                        EnableJackpotWin = c.Boolean(nullable: false),
                        JackpotPart = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerPart = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WinnersPart = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
        }
    }
}

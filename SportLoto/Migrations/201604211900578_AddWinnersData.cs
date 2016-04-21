namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWinnersData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WinnersData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(nullable: false),
                        DrawingId = c.Int(nullable: false),
                        TicketId = c.Int(nullable: false),
                        PaymentMade = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WinnersData", "TicketId", "dbo.Tickets");
            DropIndex("dbo.WinnersData", new[] { "TicketId" });
            DropTable("dbo.WinnersData");
        }
    }
}

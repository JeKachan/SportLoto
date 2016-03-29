namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullablleTicketDrawingId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "DrawingId", "dbo.Drawings");
            DropIndex("dbo.Tickets", new[] { "DrawingId" });
            AlterColumn("dbo.Tickets", "DrawingId", c => c.Int());
            CreateIndex("dbo.Tickets", "DrawingId");
            AddForeignKey("dbo.Tickets", "DrawingId", "dbo.Drawings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "DrawingId", "dbo.Drawings");
            DropIndex("dbo.Tickets", new[] { "DrawingId" });
            AlterColumn("dbo.Tickets", "DrawingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "DrawingId");
            AddForeignKey("dbo.Tickets", "DrawingId", "dbo.Drawings", "Id", cascadeDelete: true);
        }
    }
}

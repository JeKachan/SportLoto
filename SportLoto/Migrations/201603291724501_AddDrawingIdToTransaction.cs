namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDrawingIdToTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "DrawingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "DrawingId");
            AddForeignKey("dbo.Transactions", "DrawingId", "dbo.Drawings", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "DrawingId", "dbo.Drawings");
            DropIndex("dbo.Transactions", new[] { "DrawingId" });
            DropColumn("dbo.Transactions", "DrawingId");
        }
    }
}

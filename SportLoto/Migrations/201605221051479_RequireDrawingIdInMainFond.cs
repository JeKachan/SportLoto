namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequireDrawingIdInMainFond : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MainFonds", "DrawingId", "dbo.Drawings");
            DropIndex("dbo.MainFonds", new[] { "DrawingId" });
            AlterColumn("dbo.MainFonds", "DrawingId", c => c.Int(nullable: false));
            CreateIndex("dbo.MainFonds", "DrawingId");
            AddForeignKey("dbo.MainFonds", "DrawingId", "dbo.Drawings", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MainFonds", "DrawingId", "dbo.Drawings");
            DropIndex("dbo.MainFonds", new[] { "DrawingId" });
            AlterColumn("dbo.MainFonds", "DrawingId", c => c.Int());
            CreateIndex("dbo.MainFonds", "DrawingId");
            AddForeignKey("dbo.MainFonds", "DrawingId", "dbo.Drawings", "Id");
        }
    }
}

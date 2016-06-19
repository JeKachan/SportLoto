namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMainFond : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainFonds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IncrementSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DrawingId = c.Int(),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drawings", t => t.DrawingId)
                .Index(t => t.DrawingId);
            
            AddColumn("dbo.Drawings", "ToJackpotSum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Drawings", "ToWinnersSum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Drawings", "ToOwnerSum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.WinnersData", "NumberMatchCount", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MainFonds", "DrawingId", "dbo.Drawings");
            DropIndex("dbo.MainFonds", new[] { "DrawingId" });
            DropColumn("dbo.WinnersData", "NumberMatchCount");
            DropColumn("dbo.Drawings", "ToOwnerSum");
            DropColumn("dbo.Drawings", "ToWinnersSum");
            DropColumn("dbo.Drawings", "ToJackpotSum");
            DropTable("dbo.MainFonds");
        }
    }
}

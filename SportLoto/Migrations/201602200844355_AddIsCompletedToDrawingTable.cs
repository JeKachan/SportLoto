namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCompletedToDrawingTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drawings", "IsCompleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drawings", "IsCompleted");
        }
    }
}

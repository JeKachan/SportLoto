namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDefaultValueCreateDateToTicketDrawingTables : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Drawings", "CreateDate");
            DropColumn("dbo.Tickets", "CreateDate");
            AddColumn("dbo.Drawings", "CreateDate", n => n.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AddColumn("dbo.Tickets", "CreateDate", n => n.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Drawings", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}

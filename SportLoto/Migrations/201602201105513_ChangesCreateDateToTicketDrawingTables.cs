namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesCreateDateToTicketDrawingTables : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tickets", "CreateDate");
            DropColumn("dbo.Drawings", "CreateDate");
            AddColumn("dbo.Drawings", "CreateDate", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AddColumn("dbo.Tickets", "CreateDate", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "CreateDate");
            DropColumn("dbo.Drawings", "CreateDate");
        }
    }
}

namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToNullableEndDateDrawing : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Drawings", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Drawings", "EndDate", c => c.DateTime(nullable: false));
        }
    }
}

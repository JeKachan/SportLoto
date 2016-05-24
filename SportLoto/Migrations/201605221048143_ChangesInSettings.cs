namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesInSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "OwnerFond", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "OwnerFond");
        }
    }
}

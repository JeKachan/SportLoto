namespace SportLoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ItemTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 300),
                        Confirmed = c.Boolean(nullable: false),
                        PayPalTransactionId = c.String(maxLength: 50),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        CreateDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        PaymentDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
            AddColumn("dbo.Tickets", "TransactionId", c => c.Int());
            CreateIndex("dbo.Tickets", "TransactionId");
            AddForeignKey("dbo.Tickets", "TransactionId", "dbo.Transactions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "ApplicationUserId" });
            DropIndex("dbo.Tickets", new[] { "TransactionId" });
            DropColumn("dbo.Tickets", "TransactionId");
            DropTable("dbo.Transactions");
        }
    }
}

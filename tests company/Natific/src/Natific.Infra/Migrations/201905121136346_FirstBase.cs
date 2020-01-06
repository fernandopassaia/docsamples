namespace Natific.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 3),
                        Description = c.String(maxLength: 120),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 3),
                        QuantityOnHand = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedIn = c.DateTime(nullable: false),
                        UpdatedIn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.StockPiles",
                c => new
                    {
                        StockPileId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 80),
                        EntryWithDraw = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CreatedIn = c.DateTime(nullable: false),
                        UpdatedIn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StockPileId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockPiles", "ProductId", "dbo.Products");
            DropIndex("dbo.StockPiles", new[] { "ProductId" });
            DropTable("dbo.StockPiles");
            DropTable("dbo.Products");
        }
    }
}

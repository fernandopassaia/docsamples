namespace BimManufact.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Products : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Manufacturer_ManufacturerId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_ManufacturerId)
                .Index(t => t.Manufacturer_ManufacturerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Manufacturer_ManufacturerId", "dbo.Manufacturers");
            DropIndex("dbo.Products", new[] { "Manufacturer_ManufacturerId" });
            DropTable("dbo.Products");
        }
    }
}

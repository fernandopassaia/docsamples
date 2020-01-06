namespace BimManufact.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Manufacturer_ManufacturerId", "dbo.Manufacturers");
            DropIndex("dbo.Products", new[] { "Manufacturer_ManufacturerId" });
            AddColumn("dbo.Products", "ManufacturerId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Manufacturer_ManufacturerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Manufacturer_ManufacturerId", c => c.Int());
            DropColumn("dbo.Products", "ManufacturerId");
            CreateIndex("dbo.Products", "Manufacturer_ManufacturerId");
            AddForeignKey("dbo.Products", "Manufacturer_ManufacturerId", "dbo.Manufacturers", "ManufacturerId");
        }
    }
}

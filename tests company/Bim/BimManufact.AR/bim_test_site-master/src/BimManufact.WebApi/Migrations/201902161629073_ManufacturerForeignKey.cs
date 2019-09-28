namespace BimManufact.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManufacturerForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Products", "ManufacturerId");
            AddForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers", "ManufacturerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
        }
    }
}

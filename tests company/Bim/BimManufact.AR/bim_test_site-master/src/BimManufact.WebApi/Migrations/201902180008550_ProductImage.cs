namespace BimManufact.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ProductImageId = c.Int(nullable: false),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.ProductImageId)
                .ForeignKey("dbo.Products", t => t.ProductImageId)
                .Index(t => t.ProductImageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "ProductImageId", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "ProductImageId" });
            DropTable("dbo.ProductImages");
        }
    }
}

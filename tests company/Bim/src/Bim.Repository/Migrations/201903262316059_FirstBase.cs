namespace Bim.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ManufacturerImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Content = c.Binary(),
                        ManufacturerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        createBy = c.Guid(nullable: false),
                        updateBy = c.Guid(nullable: false),
                        createIn = c.DateTime(nullable: false),
                        updateIn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        createBy = c.Guid(nullable: false),
                        updateBy = c.Guid(nullable: false),
                        createIn = c.DateTime(nullable: false),
                        updateIn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Content = c.Binary(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ManufacturerImages", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.ManufacturerImages", new[] { "ManufacturerId" });
            DropTable("dbo.ProductImages");
            DropTable("dbo.Products");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.ManufacturerImages");
        }
    }
}

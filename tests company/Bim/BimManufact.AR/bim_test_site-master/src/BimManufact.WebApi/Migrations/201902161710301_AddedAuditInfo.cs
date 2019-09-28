namespace BimManufact.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAuditInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Manufacturers", "AuditCreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.Manufacturers", "AuditCreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Manufacturers", "AuditLastModifiedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.Manufacturers", "AuditLastModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "AuditCreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.Products", "AuditCreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "AuditLastModifiedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.Products", "AuditLastModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "AuditLastModifiedDate");
            DropColumn("dbo.Products", "AuditLastModifiedBy");
            DropColumn("dbo.Products", "AuditCreatedDate");
            DropColumn("dbo.Products", "AuditCreatedBy");
            DropColumn("dbo.Manufacturers", "AuditLastModifiedDate");
            DropColumn("dbo.Manufacturers", "AuditLastModifiedBy");
            DropColumn("dbo.Manufacturers", "AuditCreatedDate");
            DropColumn("dbo.Manufacturers", "AuditCreatedBy");
        }
    }
}

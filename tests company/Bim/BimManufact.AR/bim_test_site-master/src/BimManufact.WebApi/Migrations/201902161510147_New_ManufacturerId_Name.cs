namespace BimManufact.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_ManufacturerId_Name : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Manufacturers");
            DropColumn("dbo.Manufacturers", "Id");
            AddColumn("dbo.Manufacturers", "ManufacturerId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Manufacturers", "ManufacturerId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Manufacturers");
            DropColumn("dbo.Manufacturers", "ManufacturerId");
            AddColumn("dbo.Manufacturers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Manufacturers", "Id");
        }
    }
}

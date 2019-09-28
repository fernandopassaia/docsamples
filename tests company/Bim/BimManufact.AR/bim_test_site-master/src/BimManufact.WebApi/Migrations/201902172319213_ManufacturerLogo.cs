namespace BimManufact.WebApi.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ManufacturerLogo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ManufacturerLogoes",
                c => new
                    {
                        ManufacturerLogoId = c.Int(nullable: false),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.ManufacturerLogoId)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerLogoId)
                .Index(t => t.ManufacturerLogoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ManufacturerLogoes", "ManufacturerLogoId", "dbo.Manufacturers");
            DropIndex("dbo.ManufacturerLogoes", new[] { "ManufacturerLogoId" });
            DropTable("dbo.ManufacturerLogoes");
        }
    }
}

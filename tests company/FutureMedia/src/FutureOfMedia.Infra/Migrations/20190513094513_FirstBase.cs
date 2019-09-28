using System;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FutureOfMedia.Infra.Migrations
{
    public partial class FirstBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedIn = table.Column<DateTime>(nullable: false),
                    UpdatedIn = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 200, nullable: true),
                    LastName = table.Column<string>(maxLength: 200, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 200, nullable: false),
                    EmailVisible = table.Column<bool>(nullable: false),
                    PhoneVisible = table.Column<bool>(nullable: false),
                    ProfilePictureUrl = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });


            //Note by Fernando: The Task says that i should always have a First Default User, witch will be my "LoggedUser".
            //Because of That, in my first migration I'm Attaching my First User to DataBase

            StringBuilder myCommandSql = new StringBuilder();
            myCommandSql.Append("INSERT INTO[dbo].[User] ");
            myCommandSql.Append("([CreatedIn],[UpdatedIn], ");
            myCommandSql.Append("[FirstName],[LastName],[EmailAddress],[PhoneNumber], ");
            myCommandSql.Append("[EmailVisible],[PhoneVisible],[ProfilePictureUrl]) ");
            myCommandSql.Append("VALUES(GetDate(), GetDate(),'MySelf','Future Of Media' ");
            myCommandSql.Append(",'info@futureofmedia.hu','+36 20 558 2049',1,1,''); ");

            migrationBuilder.Sql(myCommandSql.ToString(), false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

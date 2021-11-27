using Microsoft.EntityFrameworkCore.Migrations;

namespace AlkemyChallenge.Migrations
{
    public partial class addEmailAddres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAdress",
                table: "RegisterUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAdress",
                table: "RegisterUsers");
        }
    }
}

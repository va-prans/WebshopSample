using Microsoft.EntityFrameworkCore.Migrations;

namespace Webshop.Persistence.Migrations
{
    public partial class requireOwnerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Accounts",
                type: "ntext",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kusys.Data.Migrations
{
    public partial class _1608225 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecretName",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecretName",
                table: "Role");
        }
    }
}

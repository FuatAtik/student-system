using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kusys.Data.Migrations
{
    public partial class _1608222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "StudentCourseMapping",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseMapping_CourseId",
                table: "StudentCourseMapping",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseMapping_Course_CourseId",
                table: "StudentCourseMapping",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseMapping_Course_CourseId",
                table: "StudentCourseMapping");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourseMapping_CourseId",
                table: "StudentCourseMapping");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "StudentCourseMapping");
        }
    }
}

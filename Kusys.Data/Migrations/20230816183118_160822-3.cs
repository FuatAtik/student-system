using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kusys.Data.Migrations
{
    public partial class _1608223 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseMapping_Course_CourseId",
                table: "StudentCourseMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseMapping_CourseCategory_CourseCategoryId",
                table: "StudentCourseMapping");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourseMapping_CourseCategoryId",
                table: "StudentCourseMapping");

            migrationBuilder.DropColumn(
                name: "CourseCategoryId",
                table: "StudentCourseMapping");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "StudentCourseMapping",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseMapping_Course_CourseId",
                table: "StudentCourseMapping",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseMapping_Course_CourseId",
                table: "StudentCourseMapping");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "StudentCourseMapping",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseCategoryId",
                table: "StudentCourseMapping",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseMapping_CourseCategoryId",
                table: "StudentCourseMapping",
                column: "CourseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseMapping_Course_CourseId",
                table: "StudentCourseMapping",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseMapping_CourseCategory_CourseCategoryId",
                table: "StudentCourseMapping",
                column: "CourseCategoryId",
                principalTable: "CourseCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

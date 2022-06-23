using Microsoft.EntityFrameworkCore.Migrations;

namespace ArelApp.DataAccess.Migrations
{
    public partial class AddingEntitiesAndRemovingEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicianId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "AcademicianId",
                table: "Exams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademicianId",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicianId",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

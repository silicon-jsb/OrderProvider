using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewOrder.Migrations
{
    /// <inheritdoc />
    public partial class SavedCourseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseTitle",
                table: "SavedCourses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseTitle",
                table: "SavedCourses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

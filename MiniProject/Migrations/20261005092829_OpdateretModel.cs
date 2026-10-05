using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProject.Migrations
{
    /// <inheritdoc />
    public partial class OpdateretModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vote",
                table: "Posts",
                newName: "Upvotes");

            migrationBuilder.RenameColumn(
                name: "Vote",
                table: "Comments",
                newName: "Upvotes");

            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Upvotes",
                table: "Posts",
                newName: "Vote");

            migrationBuilder.RenameColumn(
                name: "Upvotes",
                table: "Comments",
                newName: "Vote");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JStraining.Migrations
{
    /// <inheritdoc />
    public partial class uhakjwmd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "userLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "userLists");
        }
    }
}

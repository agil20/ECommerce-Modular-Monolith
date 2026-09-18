using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Baskets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BasketUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Baskets",
                table: "Baskets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Baskets",
                table: "Baskets");
        }
    }
}

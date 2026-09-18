using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncMassTransitV8Outbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxState_BusName_Created",
                schema: "Products",
                table: "OutboxState");

            migrationBuilder.DropColumn(
                name: "BusName",
                schema: "Products",
                table: "OutboxState");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxState_Created",
                schema: "Products",
                table: "OutboxState",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_ExpirationTime",
                schema: "Products",
                table: "OutboxMessage",
                column: "ExpirationTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_EnqueueTime",
                schema: "Products",
                table: "OutboxMessage",
                column: "EnqueueTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxState_Created",
                schema: "Products",
                table: "OutboxState");

            migrationBuilder.DropIndex(
                name: "IX_OutboxMessage_ExpirationTime",
                schema: "Products",
                table: "OutboxMessage");

            migrationBuilder.DropIndex(
                name: "IX_OutboxMessage_EnqueueTime",
                schema: "Products",
                table: "OutboxMessage");

            migrationBuilder.AddColumn<string>(
                name: "BusName",
                schema: "Products",
                table: "OutboxState",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxState_BusName_Created",
                schema: "Products",
                table: "OutboxState",
                columns: new[] { "BusName", "Created" });
        }
    }
}

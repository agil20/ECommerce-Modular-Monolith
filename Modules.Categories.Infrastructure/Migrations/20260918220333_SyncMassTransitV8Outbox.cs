using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Categories.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncMassTransitV8Outbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxState_BusName_Created",
                schema: "Categories",
                table: "OutboxState");

            migrationBuilder.DropColumn(
                name: "BusName",
                schema: "Categories",
                table: "OutboxState");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxState_Created",
                schema: "Categories",
                table: "OutboxState",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_ExpirationTime",
                schema: "Categories",
                table: "OutboxMessage",
                column: "ExpirationTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_EnqueueTime",
                schema: "Categories",
                table: "OutboxMessage",
                column: "EnqueueTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxState_Created",
                schema: "Categories",
                table: "OutboxState");

            migrationBuilder.DropIndex(
                name: "IX_OutboxMessage_ExpirationTime",
                schema: "Categories",
                table: "OutboxMessage");

            migrationBuilder.DropIndex(
                name: "IX_OutboxMessage_EnqueueTime",
                schema: "Categories",
                table: "OutboxMessage");

            migrationBuilder.AddColumn<string>(
                name: "BusName",
                schema: "Categories",
                table: "OutboxState",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxState_BusName_Created",
                schema: "Categories",
                table: "OutboxState",
                columns: new[] { "BusName", "Created" });
        }
    }
}

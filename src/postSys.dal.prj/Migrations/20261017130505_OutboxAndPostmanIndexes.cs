using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostSys.Dal.Migrations
{
    /// <inheritdoc />
    public partial class OutboxAndPostmanIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_type = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    payload = table.Column<string>(type: "text", nullable: false),
                    occurred_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    processed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_postmen_postman_email_unique",
                table: "postmen",
                column: "postman_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_postmen_postman_package_id_unique",
                table: "postmen",
                column: "postman_package_id",
                unique: true,
                filter: "postman_package_id <> '00000000-0000-0000-0000-000000000000' AND NOT is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_outbox_messages_processed_at",
                table: "outbox_messages",
                column: "processed_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropIndex(
                name: "ix_postmen_postman_email_unique",
                table: "postmen");

            migrationBuilder.DropIndex(
                name: "ix_postmen_postman_package_id_unique",
                table: "postmen");
        }
    }
}
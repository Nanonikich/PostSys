using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostSys.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_surname = table.Column<string>(type: "text", nullable: false),
                    client_name = table.Column<string>(type: "text", nullable: false),
                    client_patronymic = table.Column<string>(type: "text", nullable: true),
                    client_phone_number = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "postmen",
                columns: table => new
                {
                    postman_id = table.Column<Guid>(type: "uuid", nullable: false),
                    postman_surname = table.Column<string>(type: "text", nullable: false),
                    postman_name = table.Column<string>(type: "text", nullable: false),
                    postman_patronymic = table.Column<string>(type: "text", nullable: true),
                    postman_package_id = table.Column<Guid>(type: "uuid", nullable: false),
                    postman_email = table.Column<string>(type: "text", nullable: false),
                    postman_password = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_postmen", x => x.postman_id);
                });

            migrationBuilder.CreateTable(
                name: "packages",
                columns: table => new
                {
                    package_id = table.Column<Guid>(type: "uuid", nullable: false),
                    package_length = table.Column<double>(type: "double precision", nullable: false),
                    package_height = table.Column<double>(type: "double precision", nullable: false),
                    package_weight = table.Column<double>(type: "double precision", nullable: false),
                    package_longitude = table.Column<int>(type: "integer", nullable: false),
                    package_latitude = table.Column<int>(type: "integer", nullable: false),
                    package_status = table.Column<string>(type: "text", nullable: false),
                    package_client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    package_postman_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_packages", x => x.package_id);
                    table.ForeignKey(
                        name: "fk_packages_clients_package_client_id",
                        column: x => x.package_client_id,
                        principalTable: "clients",
                        principalColumn: "client_id");
                    table.ForeignKey(
                        name: "fk_packages_postman_package_postman_id",
                        column: x => x.package_postman_id,
                        principalTable: "postmen",
                        principalColumn: "postman_id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_packages_package_client_id",
                table: "packages",
                column: "package_client_id");

            migrationBuilder.CreateIndex(
                name: "ix_packages_package_postman_id",
                table: "packages",
                column: "package_postman_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "packages");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "postmen");
        }
    }
}

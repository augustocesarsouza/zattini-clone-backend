using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zattini.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableUserAndUserAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_zattini_users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    gender = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(299)", maxLength: 299, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    cell_phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    salt = table.Column<string>(type: "text", nullable: false),
                    user_image = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_zattini_user_addresses",
                columns: table => new
                {
                    user_addresses_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cep = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    type_address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    complement = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    neighborhood = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    state = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    city = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    reference_point = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_zattini_user_addresses", x => x.user_addresses_id);
                    table.ForeignKey(
                        name: "fk_zattini_user_addresses_user",
                        column: x => x.user_id,
                        principalTable: "tb_zattini_users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_zattini_user_addresses_user_id",
                table: "tb_zattini_user_addresses",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_zattini_user_addresses");

            migrationBuilder.DropTable(
                name: "tb_zattini_users");
        }
    }
}

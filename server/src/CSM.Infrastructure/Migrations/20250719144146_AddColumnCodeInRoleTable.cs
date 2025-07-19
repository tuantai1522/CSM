using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnCodeInRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                schema: "csm",
                table: "roles",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                schema: "csm",
                table: "roles",
                columns: new[] { "id", "code", "description", "name" },
                values: new object[] { new Guid("01982322-4302-7f89-8b24-32c5e2c0b5ff"), "R001", "To manage system", "Administrator" });

            migrationBuilder.CreateIndex(
                name: "ix_roles_code",
                schema: "csm",
                table: "roles",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_roles_code",
                schema: "csm",
                table: "roles");

            migrationBuilder.DeleteData(
                schema: "csm",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("01982322-4302-7f89-8b24-32c5e2c0b5ff"));

            migrationBuilder.DropColumn(
                name: "code",
                schema: "csm",
                table: "roles");
        }
    }
}

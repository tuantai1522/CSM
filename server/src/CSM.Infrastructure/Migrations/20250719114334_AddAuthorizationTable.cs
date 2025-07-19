using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CSM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorizationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                schema: "csm",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "views",
                schema: "csm",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    view_permission = table.Column<int>(type: "integer", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    parent_view_id = table.Column<int>(type: "integer", nullable: true),
                    url = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_views", x => x.id);
                    table.ForeignKey(
                        name: "fk_views_views_parent_view_id",
                        column: x => x.parent_view_id,
                        principalSchema: "csm",
                        principalTable: "views",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "csm",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "csm",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "csm",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                schema: "csm",
                columns: table => new
                {
                    view_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => new { x.role_id, x.view_id });
                    table.ForeignKey(
                        name: "fk_role_permissions_views_view_id",
                        column: x => x.view_id,
                        principalSchema: "csm",
                        principalTable: "views",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_permissions",
                schema: "csm",
                columns: table => new
                {
                    view_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_permissions", x => new { x.user_id, x.view_id });
                    table.ForeignKey(
                        name: "fk_user_permissions_views_view_id",
                        column: x => x.view_id,
                        principalSchema: "csm",
                        principalTable: "views",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_view_id",
                schema: "csm",
                table: "role_permissions",
                column: "view_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_permissions_view_id",
                schema: "csm",
                table: "user_permissions",
                column: "view_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                schema: "csm",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_views_parent_view_id",
                schema: "csm",
                table: "views",
                column: "parent_view_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_permissions",
                schema: "csm");

            migrationBuilder.DropTable(
                name: "user_permissions",
                schema: "csm");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "csm");

            migrationBuilder.DropTable(
                name: "views",
                schema: "csm");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "csm");
        }
    }
}

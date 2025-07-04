using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "csm");

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "csm",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                schema: "csm",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                    table.ForeignKey(
                        name: "fk_cities_countries_country_id",
                        column: x => x.country_id,
                        principalSchema: "csm",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "csm",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nick_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    first_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    last_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    email = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    hash_password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    gender_type = table.Column<string>(type: "text", nullable: false),
                    city_id = table.Column<Guid>(type: "uuid", nullable: false),
                    time_zone = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    locale = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: true),
                    deleted_at = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_cities_city_id",
                        column: x => x.city_id,
                        principalSchema: "csm",
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "channels",
                schema: "csm",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    purpose = table.Column<string>(type: "text", nullable: true),
                    last_post_at = table.Column<long>(type: "bigint", nullable: false),
                    total_post_count = table.Column<long>(type: "bigint", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: true),
                    deleted_at = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_channels", x => x.id);
                    table.ForeignKey(
                        name: "fk_channels_users_creator_id",
                        column: x => x.creator_id,
                        principalSchema: "csm",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "channel_members",
                schema: "csm",
                columns: table => new
                {
                    channel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_owner = table.Column<bool>(type: "boolean", nullable: false),
                    last_updated_at = table.Column<long>(type: "bigint", nullable: false),
                    last_viewed_at = table.Column<long>(type: "bigint", nullable: false),
                    post_count = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_channel_members", x => new { x.channel_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_channel_members_channels_channel_id",
                        column: x => x.channel_id,
                        principalSchema: "csm",
                        principalTable: "channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_channel_members_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "csm",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                schema: "csm",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    channel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    root_id = table.Column<Guid>(type: "uuid", nullable: true),
                    message = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: true),
                    deleted_at = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.id);
                    table.ForeignKey(
                        name: "fk_posts_channels_channel_id",
                        column: x => x.channel_id,
                        principalSchema: "csm",
                        principalTable: "channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_posts_posts_root_id",
                        column: x => x.root_id,
                        principalSchema: "csm",
                        principalTable: "posts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_posts_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "csm",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_channel_members_user_id",
                schema: "csm",
                table: "channel_members",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_channels_creator_id",
                schema: "csm",
                table: "channels",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_cities_country_id",
                schema: "csm",
                table: "cities",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_channel_id",
                schema: "csm",
                table: "posts",
                column: "channel_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_root_id",
                schema: "csm",
                table: "posts",
                column: "root_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_user_id",
                schema: "csm",
                table: "posts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_city_id",
                schema: "csm",
                table: "users",
                column: "city_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "channel_members",
                schema: "csm");

            migrationBuilder.DropTable(
                name: "posts",
                schema: "csm");

            migrationBuilder.DropTable(
                name: "channels",
                schema: "csm");

            migrationBuilder.DropTable(
                name: "users",
                schema: "csm");

            migrationBuilder.DropTable(
                name: "cities",
                schema: "csm");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "csm");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostAndChannelMemeberTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_channel_member_channels_channel_id",
                schema: "csm",
                table: "channel_member");

            migrationBuilder.DropForeignKey(
                name: "fk_channel_member_users_user_id",
                schema: "csm",
                table: "channel_member");

            migrationBuilder.DropForeignKey(
                name: "fk_post_channels_channel_id",
                schema: "csm",
                table: "post");

            migrationBuilder.DropForeignKey(
                name: "fk_post_post_root_id",
                schema: "csm",
                table: "post");

            migrationBuilder.DropForeignKey(
                name: "fk_post_users_user_id",
                schema: "csm",
                table: "post");

            migrationBuilder.DropPrimaryKey(
                name: "pk_post",
                schema: "csm",
                table: "post");

            migrationBuilder.DropPrimaryKey(
                name: "pk_channel_member",
                schema: "csm",
                table: "channel_member");

            migrationBuilder.RenameTable(
                name: "post",
                schema: "csm",
                newName: "posts",
                newSchema: "csm");

            migrationBuilder.RenameTable(
                name: "channel_member",
                schema: "csm",
                newName: "channel_members",
                newSchema: "csm");

            migrationBuilder.RenameIndex(
                name: "ix_post_user_id",
                schema: "csm",
                table: "posts",
                newName: "ix_posts_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_root_id",
                schema: "csm",
                table: "posts",
                newName: "ix_posts_root_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_channel_id",
                schema: "csm",
                table: "posts",
                newName: "ix_posts_channel_id");

            migrationBuilder.RenameIndex(
                name: "ix_channel_member_user_id",
                schema: "csm",
                table: "channel_members",
                newName: "ix_channel_members_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_posts",
                schema: "csm",
                table: "posts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_channel_members",
                schema: "csm",
                table: "channel_members",
                columns: new[] { "channel_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "ix_posts_created_at_id",
                schema: "csm",
                table: "posts",
                columns: new[] { "created_at", "id" });

            migrationBuilder.AddForeignKey(
                name: "fk_channel_members_channels_channel_id",
                schema: "csm",
                table: "channel_members",
                column: "channel_id",
                principalSchema: "csm",
                principalTable: "channels",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_channel_members_users_user_id",
                schema: "csm",
                table: "channel_members",
                column: "user_id",
                principalSchema: "csm",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_posts_channels_channel_id",
                schema: "csm",
                table: "posts",
                column: "channel_id",
                principalSchema: "csm",
                principalTable: "channels",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_posts_posts_root_id",
                schema: "csm",
                table: "posts",
                column: "root_id",
                principalSchema: "csm",
                principalTable: "posts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_posts_users_user_id",
                schema: "csm",
                table: "posts",
                column: "user_id",
                principalSchema: "csm",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_channel_members_channels_channel_id",
                schema: "csm",
                table: "channel_members");

            migrationBuilder.DropForeignKey(
                name: "fk_channel_members_users_user_id",
                schema: "csm",
                table: "channel_members");

            migrationBuilder.DropForeignKey(
                name: "fk_posts_channels_channel_id",
                schema: "csm",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "fk_posts_posts_root_id",
                schema: "csm",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "fk_posts_users_user_id",
                schema: "csm",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_posts",
                schema: "csm",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "ix_posts_created_at_id",
                schema: "csm",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_channel_members",
                schema: "csm",
                table: "channel_members");

            migrationBuilder.RenameTable(
                name: "posts",
                schema: "csm",
                newName: "post",
                newSchema: "csm");

            migrationBuilder.RenameTable(
                name: "channel_members",
                schema: "csm",
                newName: "channel_member",
                newSchema: "csm");

            migrationBuilder.RenameIndex(
                name: "ix_posts_user_id",
                schema: "csm",
                table: "post",
                newName: "ix_post_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_posts_root_id",
                schema: "csm",
                table: "post",
                newName: "ix_post_root_id");

            migrationBuilder.RenameIndex(
                name: "ix_posts_channel_id",
                schema: "csm",
                table: "post",
                newName: "ix_post_channel_id");

            migrationBuilder.RenameIndex(
                name: "ix_channel_members_user_id",
                schema: "csm",
                table: "channel_member",
                newName: "ix_channel_member_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_post",
                schema: "csm",
                table: "post",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_channel_member",
                schema: "csm",
                table: "channel_member",
                columns: new[] { "channel_id", "user_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_channel_member_channels_channel_id",
                schema: "csm",
                table: "channel_member",
                column: "channel_id",
                principalSchema: "csm",
                principalTable: "channels",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_channel_member_users_user_id",
                schema: "csm",
                table: "channel_member",
                column: "user_id",
                principalSchema: "csm",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_channels_channel_id",
                schema: "csm",
                table: "post",
                column: "channel_id",
                principalSchema: "csm",
                principalTable: "channels",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_post_root_id",
                schema: "csm",
                table: "post",
                column: "root_id",
                principalSchema: "csm",
                principalTable: "post",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_users_user_id",
                schema: "csm",
                table: "post",
                column: "user_id",
                principalSchema: "csm",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

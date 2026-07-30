using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePrep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddResourceAndUserTopicProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Topics_TopicId",
                table: "ResourceLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceLink",
                table: "ResourceLink");

            migrationBuilder.RenameTable(
                name: "ResourceLink",
                newName: "ResourceLinks");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceLink_TopicId",
                table: "ResourceLinks",
                newName: "IX_ResourceLinks_TopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceLinks",
                table: "ResourceLinks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserTopicProgresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsBookmarked = table.Column<bool>(type: "bit", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopicProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTopicProgresses_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicProgresses_TopicId",
                table: "UserTopicProgresses",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLinks_Topics_TopicId",
                table: "ResourceLinks",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLinks_Topics_TopicId",
                table: "ResourceLinks");

            migrationBuilder.DropTable(
                name: "UserTopicProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceLinks",
                table: "ResourceLinks");

            migrationBuilder.RenameTable(
                name: "ResourceLinks",
                newName: "ResourceLink");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceLinks_TopicId",
                table: "ResourceLink",
                newName: "IX_ResourceLink_TopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceLink",
                table: "ResourceLink",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Topics_TopicId",
                table: "ResourceLink",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

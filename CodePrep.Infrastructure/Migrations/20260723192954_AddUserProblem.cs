using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePrep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProblems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSolved = table.Column<bool>(type: "bit", nullable: false),
                    IsBookmarked = table.Column<bool>(type: "bit", nullable: false),
                    SolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProblems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProblems_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProblems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProblems_ProblemId",
                table: "UserProblems",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProblems_UserId",
                table: "UserProblems",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProblems");
        }
    }
}

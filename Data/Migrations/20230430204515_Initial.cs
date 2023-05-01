using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorID = table.Column<int>(type: "INTEGER", nullable: true),
                    PostID = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorID = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Points = table.Column<decimal>(type: "decimal(6, 1)", nullable: false),
                    CommentID = table.Column<int>(type: "INTEGER", nullable: true),
                    PostID = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Comments_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorID",
                table: "Comments",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorID",
                table: "Posts",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CommentID",
                table: "Users",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PostID",
                table: "Users",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostID",
                table: "Comments",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_AuthorID",
                table: "Comments",
                column: "AuthorID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorID",
                table: "Posts",
                column: "AuthorID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Posts_PostID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_AuthorID",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}

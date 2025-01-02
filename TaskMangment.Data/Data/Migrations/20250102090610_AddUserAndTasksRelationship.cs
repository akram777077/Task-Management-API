using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMangment.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndTasksRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ToDoItems",
                type: "character varying(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserName);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_Username",
                table: "ToDoItems",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_Users_Username",
                table: "ToDoItems",
                column: "Username",
                principalTable: "Users",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_Users_Username",
                table: "ToDoItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_Username",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "ToDoItems");
        }
    }
}

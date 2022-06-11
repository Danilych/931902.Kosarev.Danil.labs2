using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web6.Migrations
{
    public partial class InitialCreateAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForumCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TopicCreator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicCreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepliesAmount = table.Column<int>(type: "int", nullable: false),
                    LasReplyCreator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastReplyDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForumCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topic_ForumCategory_ForumCategoryId",
                        column: x => x.ForumCategoryId,
                        principalTable: "ForumCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TopicPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicMessage1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicMessage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicCreator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicCreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicEditDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicPost_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "user" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Password", "RoleId" },
                values: new object[] { 1, "admin@mail.ru", "123456", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Topic_ForumCategoryId",
                table: "Topic",
                column: "ForumCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicPost_TopicId",
                table: "TopicPost",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicPost");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "ForumCategory");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web6.Migrations
{
    public partial class AddMainPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicMessage1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicMessage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicCreator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicCreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicEditDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainPost", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainPost");
        }
    }
}

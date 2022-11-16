using Microsoft.EntityFrameworkCore.Migrations;

namespace UserStory_Men_Cher.Migrations
{
    public partial class AddChtoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KolvoPalcev",
                table: "KiselDB",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KolvoPalcev",
                table: "KiselDB");
        }
    }
}

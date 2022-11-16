using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserStory_Men_Cher.Migrations
{
    public partial class Start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KiselDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    formStudy = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    Math = table.Column<int>(nullable: false),
                    Russia = table.Column<int>(nullable: false),
                    Inform = table.Column<int>(nullable: false),
                    Sum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KiselDB", x => x.Id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KiselDB");
        }
    }
}

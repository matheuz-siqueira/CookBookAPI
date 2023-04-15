using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cookbook_api.Migrations
{
    public partial class AddPreparationTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreparationTime",
                table: "Recipe",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "Recipe");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlanAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Foods",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Foods");
        }
    }
}

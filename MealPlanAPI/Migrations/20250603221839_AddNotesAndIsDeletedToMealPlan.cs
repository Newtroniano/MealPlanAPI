using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlanAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesAndIsDeletedToMealPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "MealPlans");

            migrationBuilder.DropColumn(
                name: "PortionSizeInGrams",
                table: "MealPlanFoods");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "MealPlans",
                newName: "Notes");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MealPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MealPlanFoods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MealTime",
                table: "MealPlanFoods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PortionSize",
                table: "MealPlanFoods",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MealPlans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MealPlanFoods");

            migrationBuilder.DropColumn(
                name: "MealTime",
                table: "MealPlanFoods");

            migrationBuilder.DropColumn(
                name: "PortionSize",
                table: "MealPlanFoods");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "MealPlans",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MealPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PortionSizeInGrams",
                table: "MealPlanFoods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

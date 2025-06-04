using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlanAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb71c649-33b5-476e-8d20-2a49610a5e87", "AQAAAAIAAYagAAAAEAA7ydj55tHw7Iq1+v6jweJhDL/gEmrnit+bQYlq1erXwodBDuLWL9kcGoxuPIjKZQ==", "436a2dd5-9149-46de-82f6-9a16dc51e0db" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8ce622f-ba4f-475a-95fe-b50d9bf4c914", "AQAAAAIAAYagAAAAEHAclXlcxE0FHFlBNp/ceRXtXz8pnIW71lz36ALwEdoDhvR4yNNL5Oo8An9zM2dk3Q==", "e0c79607-6e7d-4d4b-83ad-70f8a6302cd2" });
        }
    }
}

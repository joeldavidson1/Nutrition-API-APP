using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionAPI.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7914e0ec-0206-4c1c-9224-38e79e390cbb", "6b9a1cf1-fff7-4233-8e2f-14c9713db5a1", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7914e0ec-0206-4c1c-9224-38e79e390cbb");
        }
    }
}

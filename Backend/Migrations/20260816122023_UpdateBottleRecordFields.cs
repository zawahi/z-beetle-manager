using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuwagataApp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBottleRecordFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaName",
                table: "BottleRecords");

            migrationBuilder.RenameColumn(
                name: "MediaType",
                table: "BottleRecords",
                newName: "Capacity");

            migrationBuilder.AddColumn<string>(
                name: "BottleType",
                table: "BottleRecords",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BottleType",
                table: "BottleRecords");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "BottleRecords",
                newName: "MediaType");

            migrationBuilder.AddColumn<string>(
                name: "MediaName",
                table: "BottleRecords",
                type: "text",
                nullable: true);
        }
    }
}

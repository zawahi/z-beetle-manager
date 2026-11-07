using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuwagataApp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBreedingToTextInputs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreedingRecords_Beetles_FemaleId",
                table: "BreedingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_BreedingRecords_Beetles_MaleId",
                table: "BreedingRecords");

            migrationBuilder.DropIndex(
                name: "IX_BreedingRecords_FemaleId",
                table: "BreedingRecords");

            migrationBuilder.DropIndex(
                name: "IX_BreedingRecords_MaleId",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "FemaleId",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "MaleId",
                table: "BreedingRecords");

            migrationBuilder.AddColumn<string>(
                name: "FemaleName",
                table: "BreedingRecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaleName",
                table: "BreedingRecords",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FemaleName",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "MaleName",
                table: "BreedingRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "FemaleId",
                table: "BreedingRecords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MaleId",
                table: "BreedingRecords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BreedingRecords_FemaleId",
                table: "BreedingRecords",
                column: "FemaleId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingRecords_MaleId",
                table: "BreedingRecords",
                column: "MaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BreedingRecords_Beetles_FemaleId",
                table: "BreedingRecords",
                column: "FemaleId",
                principalTable: "Beetles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BreedingRecords_Beetles_MaleId",
                table: "BreedingRecords",
                column: "MaleId",
                principalTable: "Beetles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

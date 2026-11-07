using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuwagataApp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beetles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Species = table.Column<string>(type: "text", nullable: false),
                    Locality = table.Column<string>(type: "text", nullable: true),
                    Generation = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Stage = table.Column<int>(type: "integer", nullable: false),
                    HatchDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmergenceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Size = table.Column<decimal>(type: "numeric", nullable: true),
                    SireId = table.Column<Guid>(type: "uuid", nullable: true),
                    DamId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beetles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beetles_Beetles_DamId",
                        column: x => x.DamId,
                        principalTable: "Beetles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Beetles_Beetles_SireId",
                        column: x => x.SireId,
                        principalTable: "Beetles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BottleRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BeetleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExchangeDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MediaType = table.Column<int>(type: "integer", nullable: false),
                    MediaName = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: true),
                    HeadWidth = table.Column<decimal>(type: "numeric", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BottleRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BottleRecords_Beetles_BeetleId",
                        column: x => x.BeetleId,
                        principalTable: "Beetles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BreedingRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    FemaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SetDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AllocationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EggCount = table.Column<int>(type: "integer", nullable: true),
                    LarvaCount = table.Column<int>(type: "integer", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreedingRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreedingRecords_Beetles_FemaleId",
                        column: x => x.FemaleId,
                        principalTable: "Beetles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BreedingRecords_Beetles_MaleId",
                        column: x => x.MaleId,
                        principalTable: "Beetles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beetles_DamId",
                table: "Beetles",
                column: "DamId");

            migrationBuilder.CreateIndex(
                name: "IX_Beetles_SireId",
                table: "Beetles",
                column: "SireId");

            migrationBuilder.CreateIndex(
                name: "IX_BottleRecords_BeetleId",
                table: "BottleRecords",
                column: "BeetleId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingRecords_FemaleId",
                table: "BreedingRecords",
                column: "FemaleId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingRecords_MaleId",
                table: "BreedingRecords",
                column: "MaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BottleRecords");

            migrationBuilder.DropTable(
                name: "BreedingRecords");

            migrationBuilder.DropTable(
                name: "Beetles");
        }
    }
}

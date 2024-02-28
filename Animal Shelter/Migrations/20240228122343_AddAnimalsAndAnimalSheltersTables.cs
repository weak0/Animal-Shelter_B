using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animal_Shelter.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalsAndAnimalSheltersTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalShelters",
                columns: table => new
                {
                    AnimalShelterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalShelterName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalShelters", x => x.AnimalShelterId);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalShelterNameAnimalShelterId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.AnimalId);
                    table.ForeignKey(
                        name: "FK_Animals_AnimalShelters_AnimalShelterNameAnimalShelterId",
                        column: x => x.AnimalShelterNameAnimalShelterId,
                        principalTable: "AnimalShelters",
                        principalColumn: "AnimalShelterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AnimalShelterNameAnimalShelterId",
                table: "Animals",
                column: "AnimalShelterNameAnimalShelterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "AnimalShelters");
        }
    }
}

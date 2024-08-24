using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnakeNet_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enclosures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lenght = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enclosures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnclosureReading",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnclosureSide = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnclosureId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnclosureReading", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnclosureReading_Enclosures_EnclosureId",
                        column: x => x.EnclosureId,
                        principalTable: "Enclosures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Snakes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snakes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Snakes_Enclosures_Id",
                        column: x => x.Id,
                        principalTable: "Enclosures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eliminations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Healthy = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SnakeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eliminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eliminations_Snakes_SnakeId",
                        column: x => x.SnakeId,
                        principalTable: "Snakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedingRecord",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FeederWeight = table.Column<int>(type: "int", nullable: false),
                    Feeder = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SnakeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedingRecord_Snakes_SnakeId",
                        column: x => x.SnakeId,
                        principalTable: "Snakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrowthRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lenght = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SnakeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrowthRecords_Snakes_SnakeId",
                        column: x => x.SnakeId,
                        principalTable: "Snakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eliminations_SnakeId",
                table: "Eliminations",
                column: "SnakeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnclosureReading_EnclosureId",
                table: "EnclosureReading",
                column: "EnclosureId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingRecord_SnakeId",
                table: "FeedingRecord",
                column: "SnakeId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthRecords_SnakeId",
                table: "GrowthRecords",
                column: "SnakeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eliminations");

            migrationBuilder.DropTable(
                name: "EnclosureReading");

            migrationBuilder.DropTable(
                name: "FeedingRecord");

            migrationBuilder.DropTable(
                name: "GrowthRecords");

            migrationBuilder.DropTable(
                name: "Snakes");

            migrationBuilder.DropTable(
                name: "Enclosures");
        }
    }
}

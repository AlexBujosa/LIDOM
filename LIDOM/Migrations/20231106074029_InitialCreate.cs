using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LIDOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LidomTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Home = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LidomTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_FirstTeam = table.Column<int>(type: "int", nullable: false),
                    Id_SecondTeam = table.Column<int>(type: "int", nullable: false),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Home = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendars_LidomTeams_Id_FirstTeam",
                        column: x => x.Id_FirstTeam,
                        principalTable: "LidomTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calendars_LidomTeams_Id_SecondTeam",
                        column: x => x.Id_SecondTeam,
                        principalTable: "LidomTeams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stadistics",
                columns: table => new
                {
                    Id_Calendar = table.Column<int>(type: "int", nullable: false),
                    Id_Team = table.Column<int>(type: "int", nullable: false),
                    Win = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadistics", x => new { x.Id_Calendar, x.Id_Team });
                    table.ForeignKey(
                        name: "FK_Stadistics_Calendars_Id_Calendar",
                        column: x => x.Id_Calendar,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stadistics_LidomTeams_Id_Team",
                        column: x => x.Id_Team,
                        principalTable: "LidomTeams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_Id_FirstTeam",
                table: "Calendars",
                column: "Id_FirstTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_Id_SecondTeam",
                table: "Calendars",
                column: "Id_SecondTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Stadistics_Id_Team",
                table: "Stadistics",
                column: "Id_Team");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stadistics");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "LidomTeams");
        }
    }
}

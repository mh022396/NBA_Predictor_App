using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamesPlayed = table.Column<int>(type: "int", nullable: false),
                    MinutesPlayed = table.Column<float>(type: "real", nullable: false),
                    PointsPerGame = table.Column<float>(type: "real", nullable: false),
                    ReboundsPerGame = table.Column<float>(type: "real", nullable: false),
                    AssistsPerGame = table.Column<float>(type: "real", nullable: false),
                    StealsPerGame = table.Column<float>(type: "real", nullable: false),
                    BlocksPerGame = table.Column<float>(type: "real", nullable: false),
                    FieldGoalPercentage = table.Column<float>(type: "real", nullable: false),
                    ThreePointPercentage = table.Column<float>(type: "real", nullable: false),
                    FreeThrowPercentage = table.Column<float>(type: "real", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentPlayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PastMVPs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamesPlayed = table.Column<int>(type: "int", nullable: false),
                    MinutesPlayed = table.Column<float>(type: "real", nullable: false),
                    PointsPerGame = table.Column<float>(type: "real", nullable: false),
                    ReboundsPerGame = table.Column<float>(type: "real", nullable: false),
                    AssistsPerGame = table.Column<float>(type: "real", nullable: false),
                    StealsPerGame = table.Column<float>(type: "real", nullable: false),
                    BlocksPerGame = table.Column<float>(type: "real", nullable: false),
                    FieldGoalPercentage = table.Column<float>(type: "real", nullable: false),
                    ThreePointPercentage = table.Column<float>(type: "real", nullable: false),
                    FreeThrowPercentage = table.Column<float>(type: "real", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MVPShare = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastMVPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PastPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<float>(type: "real", nullable: false),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamesPlayed = table.Column<float>(type: "real", nullable: false),
                    MinutesPlayed = table.Column<float>(type: "real", nullable: false),
                    PointsPerGame = table.Column<float>(type: "real", nullable: false),
                    ReboundsPerGame = table.Column<float>(type: "real", nullable: false),
                    AssistsPerGame = table.Column<float>(type: "real", nullable: false),
                    StealsPerGame = table.Column<float>(type: "real", nullable: false),
                    BlocksPerGame = table.Column<float>(type: "real", nullable: false),
                    FieldGoalPercentage = table.Column<float>(type: "real", nullable: false),
                    ThreePointPercentage = table.Column<float>(type: "real", nullable: false),
                    FreeThrowPercentage = table.Column<float>(type: "real", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastPlayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Predictions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredictedShare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PredictedRank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentPlayers");

            migrationBuilder.DropTable(
                name: "PastMVPs");

            migrationBuilder.DropTable(
                name: "PastPlayers");

            migrationBuilder.DropTable(
                name: "Predictions");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoApptechBackend.Migrations
{
    /// <inheritdoc />
    public partial class firstSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeRanks",
                columns: table => new
                {
                    EmployeeRankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequiredPoints = table.Column<int>(type: "int", nullable: false),
                    EmployeeRankRankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRanks", x => x.EmployeeRankID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    YearsInPratice = table.Column<double>(type: "float", nullable: false),
                    EmpPoints = table.Column<int>(type: "int", nullable: false),
                    PongPoints = table.Column<int>(type: "int", nullable: false),
                    LossesInPingPong = table.Column<int>(type: "int", nullable: false),
                    WinningsInPingPong = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "PingPongRanks",
                columns: table => new
                {
                    PingPongRankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequiredPoints = table.Column<int>(type: "int", nullable: false),
                    PingPongRankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PingPongRanks", x => x.PingPongRankID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRanks");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "PingPongRanks");
        }
    }
}

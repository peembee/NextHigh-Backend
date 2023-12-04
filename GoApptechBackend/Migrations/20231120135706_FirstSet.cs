using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoApptechBackend.Migrations
{
    /// <inheritdoc />
    public partial class FirstSet : Migration
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
                    EmployeeRankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRanks", x => x.EmployeeRankID);
                });

            migrationBuilder.CreateTable(
                name: "PingPongRanks",
                columns: table => new
                {
                    PingPongRankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PingPongRankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PingPongRanks", x => x.PingPongRankID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePoints",
                columns: table => new
                {
                    EmployeePointsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Points = table.Column<int>(type: "int", nullable: false),
                    FK_EmployeeRankID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePoints", x => x.EmployeePointsID);
                    table.ForeignKey(
                        name: "FK_EmployeePoints_EmployeeRanks_FK_EmployeeRankID",
                        column: x => x.FK_EmployeeRankID,
                        principalTable: "EmployeeRanks",
                        principalColumn: "EmployeeRankID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PingPongPoints",
                columns: table => new
                {
                    PingPongPointsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Points = table.Column<int>(type: "int", nullable: false),
                    FK_PongRankID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PingPongPoints", x => x.PingPongPointsID);
                    table.ForeignKey(
                        name: "FK_PingPongPoints_PingPongRanks_FK_PongRankID",
                        column: x => x.FK_PongRankID,
                        principalTable: "PingPongRanks",
                        principalColumn: "PingPongRankID",
                        onDelete: ReferentialAction.Cascade);
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
                    FK_EmployeePointsID = table.Column<int>(type: "int", nullable: true),
                    PongPoints = table.Column<int>(type: "int", nullable: false),
                    FK_PingPongPointsID = table.Column<int>(type: "int", nullable: true),
                    LossesInPingPong = table.Column<int>(type: "int", nullable: true),
                    WinningsInPingPong = table.Column<int>(type: "int", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Persons_EmployeePoints_FK_EmployeePointsID",
                        column: x => x.FK_EmployeePointsID,
                        principalTable: "EmployeePoints",
                        principalColumn: "EmployeePointsID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_PingPongPoints_FK_PingPongPointsID",
                        column: x => x.FK_PingPongPointsID,
                        principalTable: "PingPongPoints",
                        principalColumn: "PingPongPointsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePoints_FK_EmployeeRankID",
                table: "EmployeePoints",
                column: "FK_EmployeeRankID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FK_EmployeePointsID",
                table: "Persons",
                column: "FK_EmployeePointsID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FK_PingPongPointsID",
                table: "Persons",
                column: "FK_PingPongPointsID");

            migrationBuilder.CreateIndex(
                name: "IX_PingPongPoints_FK_PongRankID",
                table: "PingPongPoints",
                column: "FK_PongRankID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "EmployeePoints");

            migrationBuilder.DropTable(
                name: "PingPongPoints");

            migrationBuilder.DropTable(
                name: "EmployeeRanks");

            migrationBuilder.DropTable(
                name: "PingPongRanks");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoApptechBackend.Migrations
{
    /// <inheritdoc />
    public partial class newSetup : Migration
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
                    RankTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequiredPoints = table.Column<int>(type: "int", nullable: false)
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
                    RankTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequiredWinnings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PingPongRanks", x => x.PingPongRankID);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizHeading = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AltOne = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AltTwo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AltThree = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_PingPongRankID = table.Column<int>(type: "int", nullable: false),
                    FK_EmployeeRankID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    YearsInPratice = table.Column<double>(type: "float", nullable: false),
                    EmpPoints = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Persons_EmployeeRanks_FK_EmployeeRankID",
                        column: x => x.FK_EmployeeRankID,
                        principalTable: "EmployeeRanks",
                        principalColumn: "EmployeeRankID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_PingPongRanks_FK_PingPongRankID",
                        column: x => x.FK_PingPongRankID,
                        principalTable: "PingPongRanks",
                        principalColumn: "PingPongRankID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeResults",
                columns: table => new
                {
                    EmployeeResultID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_PersonID = table.Column<int>(type: "int", nullable: false),
                    FK_QuizID = table.Column<int>(type: "int", nullable: false),
                    QuizDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeResults", x => x.EmployeeResultID);
                    table.ForeignKey(
                        name: "FK_EmployeeResults_Persons_FK_PersonID",
                        column: x => x.FK_PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeResults_Quizzes_FK_QuizID",
                        column: x => x.FK_QuizID,
                        principalTable: "Quizzes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PingPongResults",
                columns: table => new
                {
                    PingPongResultID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_PersonID = table.Column<int>(type: "int", nullable: false),
                    FK_PersonIDPoints = table.Column<int>(type: "int", nullable: false),
                    OpponentPoints = table.Column<int>(type: "int", nullable: false),
                    WonMatch = table.Column<bool>(type: "bit", nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PingPongResults", x => x.PingPongResultID);
                    table.ForeignKey(
                        name: "FK_PingPongResults_Persons_FK_PersonID",
                        column: x => x.FK_PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeResults_FK_PersonID",
                table: "EmployeeResults",
                column: "FK_PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeResults_FK_QuizID",
                table: "EmployeeResults",
                column: "FK_QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FK_EmployeeRankID",
                table: "Persons",
                column: "FK_EmployeeRankID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FK_PingPongRankID",
                table: "Persons",
                column: "FK_PingPongRankID");

            migrationBuilder.CreateIndex(
                name: "IX_PingPongResults_FK_PersonID",
                table: "PingPongResults",
                column: "FK_PersonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeResults");

            migrationBuilder.DropTable(
                name: "PingPongResults");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "EmployeeRanks");

            migrationBuilder.DropTable(
                name: "PingPongRanks");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoApptechBackend.Migrations
{
    /// <inheritdoc />
    public partial class addedpingPongResultGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatchGuid",
                table: "PingPongResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchGuid",
                table: "PingPongResults");
        }
    }
}

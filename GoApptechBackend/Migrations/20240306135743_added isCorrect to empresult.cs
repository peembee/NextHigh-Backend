﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoApptechBackend.Migrations
{
    /// <inheritdoc />
    public partial class addedisCorrecttoempresult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCorrect",
                table: "EmployeeResults",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCorrect",
                table: "EmployeeResults");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obelyx.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Games");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedDate",
                table: "Games",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Games",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedDate",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Games",
                type: "int",
                nullable: true);
        }
    }
}

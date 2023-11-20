using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HauCK.Migrations
{
    /// <inheritdoc />
    public partial class update_date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Assigners");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdate",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "AssignmentID",
                table: "Assigners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignmentID",
                table: "Assigners");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Assigners",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

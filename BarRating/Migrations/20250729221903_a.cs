using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarRating.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "ScheduleOverrides");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ScheduleOverrides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "ScheduleOverrides");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "ScheduleOverrides",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

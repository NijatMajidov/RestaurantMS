using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class debug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 28, 15, 28, 23, 983, DateTimeKind.Utc).AddTicks(3535),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 28, 12, 4, 41, 285, DateTimeKind.Utc).AddTicks(9027));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 28, 12, 4, 41, 285, DateTimeKind.Utc).AddTicks(9027),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 28, 15, 28, 23, 983, DateTimeKind.Utc).AddTicks(3535));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class debuging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 28, 15, 29, 54, 515, DateTimeKind.Utc).AddTicks(6404),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 28, 15, 28, 23, 983, DateTimeKind.Utc).AddTicks(3535));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 28, 15, 28, 23, 983, DateTimeKind.Utc).AddTicks(3535),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 28, 15, 29, 54, 515, DateTimeKind.Utc).AddTicks(6404));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableSlidesAdds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Tables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 30, 0, 34, 6, 11, DateTimeKind.Utc).AddTicks(9702),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 29, 16, 10, 28, 928, DateTimeKind.Utc).AddTicks(6624));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Slides",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Slides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 30, 0, 34, 6, 11, DateTimeKind.Utc).AddTicks(6167),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 29, 16, 10, 28, 927, DateTimeKind.Utc).AddTicks(9021));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 30, 0, 34, 6, 11, DateTimeKind.Utc).AddTicks(2255),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 29, 16, 10, 28, 927, DateTimeKind.Utc).AddTicks(1048));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Tables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 29, 16, 10, 28, 928, DateTimeKind.Utc).AddTicks(6624),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 30, 0, 34, 6, 11, DateTimeKind.Utc).AddTicks(9702));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Slides",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Slides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 29, 16, 10, 28, 927, DateTimeKind.Utc).AddTicks(9021),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 30, 0, 34, 6, 11, DateTimeKind.Utc).AddTicks(6167));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 29, 16, 10, 28, 927, DateTimeKind.Utc).AddTicks(1048),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 30, 0, 34, 6, 11, DateTimeKind.Utc).AddTicks(2255));
        }
    }
}

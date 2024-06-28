using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Tables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 0, 29, 6, 704, DateTimeKind.Utc).AddTicks(9304),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 203, DateTimeKind.Utc).AddTicks(492));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Slides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 0, 29, 6, 704, DateTimeKind.Utc).AddTicks(3351),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 202, DateTimeKind.Utc).AddTicks(4110));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ReservationTime",
                table: "Reservations",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(1, 0, 29, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldDefaultValue: new TimeSpan(0, 23, 39, 0, 0));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReservationDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 0, 29, 6, 703, DateTimeKind.Utc).AddTicks(4983),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 201, DateTimeKind.Utc).AddTicks(5504));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 0, 29, 6, 702, DateTimeKind.Utc).AddTicks(4707),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 200, DateTimeKind.Utc).AddTicks(8591));

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biography",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Tables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 203, DateTimeKind.Utc).AddTicks(492),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 0, 29, 6, 704, DateTimeKind.Utc).AddTicks(9304));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Slides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 202, DateTimeKind.Utc).AddTicks(4110),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 0, 29, 6, 704, DateTimeKind.Utc).AddTicks(3351));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ReservationTime",
                table: "Reservations",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 23, 39, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldDefaultValue: new TimeSpan(1, 0, 29, 0, 0));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReservationDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 201, DateTimeKind.Utc).AddTicks(5504),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 0, 29, 6, 703, DateTimeKind.Utc).AddTicks(4983));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 200, DateTimeKind.Utc).AddTicks(8591),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 0, 29, 6, 702, DateTimeKind.Utc).AddTicks(4707));
        }
    }
}

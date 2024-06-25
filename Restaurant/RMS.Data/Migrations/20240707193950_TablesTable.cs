﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class TablesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Tables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 203, DateTimeKind.Utc).AddTicks(492),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 30, 22, 38, 29, 166, DateTimeKind.Utc).AddTicks(5244));

            migrationBuilder.AddColumn<string>(
                name: "WaiterId",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Slides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 202, DateTimeKind.Utc).AddTicks(4110),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 30, 22, 38, 29, 165, DateTimeKind.Utc).AddTicks(9379));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ReservationTime",
                table: "Reservations",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 23, 39, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldDefaultValue: new TimeSpan(0, 22, 38, 0, 0));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReservationDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 201, DateTimeKind.Utc).AddTicks(5504),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 30, 22, 38, 29, 165, DateTimeKind.Utc).AddTicks(4203));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 200, DateTimeKind.Utc).AddTicks(8591),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 30, 22, 38, 29, 164, DateTimeKind.Utc).AddTicks(7135));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WaiterId",
                table: "Tables");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Tables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 30, 22, 38, 29, 166, DateTimeKind.Utc).AddTicks(5244),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 203, DateTimeKind.Utc).AddTicks(492));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Slides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 30, 22, 38, 29, 165, DateTimeKind.Utc).AddTicks(9379),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 202, DateTimeKind.Utc).AddTicks(4110));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ReservationTime",
                table: "Reservations",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 22, 38, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldDefaultValue: new TimeSpan(0, 23, 39, 0, 0));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReservationDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 30, 22, 38, 29, 165, DateTimeKind.Utc).AddTicks(4203),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 201, DateTimeKind.Utc).AddTicks(5504));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 30, 22, 38, 29, 164, DateTimeKind.Utc).AddTicks(7135),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 23, 39, 49, 200, DateTimeKind.Utc).AddTicks(8591));
        }
    }
}

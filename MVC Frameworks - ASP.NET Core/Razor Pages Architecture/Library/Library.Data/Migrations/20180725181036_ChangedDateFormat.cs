using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Data.Migrations
{
    public partial class ChangedDateFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Records",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 25, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 7, 25, 18, 6, 13, 56, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Records",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 25, 18, 6, 13, 56, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 7, 25, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}

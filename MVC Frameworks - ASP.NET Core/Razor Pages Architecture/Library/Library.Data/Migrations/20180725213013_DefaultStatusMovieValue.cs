using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Data.Migrations
{
    public partial class DefaultStatusMovieValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Records",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 26, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 7, 25, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Movies",
                nullable: true,
                defaultValue: "At home",
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Records",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 25, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 7, 26, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "At home");
        }
    }
}

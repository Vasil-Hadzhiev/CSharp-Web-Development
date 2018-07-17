﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLibrary.Data.Migrations
{
    public partial class DefaultStatusValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Books",
                nullable: true,
                defaultValue: "At home",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValueSql: "Home");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Books",
                nullable: true,
                defaultValueSql: "Home",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "At home");
        }
    }
}

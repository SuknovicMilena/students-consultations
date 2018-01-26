using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentsConsultations.Data.EF.Migrations
{
    public partial class AbstractedKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrojIndeksa",
                table: "Student",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BrojRadneKnjizice",
                table: "Nastavnik",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojIndeksa",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "BrojRadneKnjizice",
                table: "Nastavnik");
        }
    }
}

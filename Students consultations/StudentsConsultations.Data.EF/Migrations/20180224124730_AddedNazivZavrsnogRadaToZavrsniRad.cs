using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentsConsultations.Data.EF.Migrations
{
    public partial class AddedNazivZavrsnogRadaToZavrsniRad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NazivZavrsnogRada",
                table: "ZavrsniRad",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NazivZavrsnogRada",
                table: "ZavrsniRad");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentsConsultations.Data.EF.Migrations
{
    public partial class AddedOdrzaneToKonsultacije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Odrzane",
                table: "Konsultacije",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Odrzane",
                table: "Konsultacije");
        }
    }
}

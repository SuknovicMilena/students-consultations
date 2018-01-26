using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentsConsultations.Data.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Datum",
                columns: table => new
                {
                    DatumKonsultacija = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datum", x => x.DatumKonsultacija);
                });

            migrationBuilder.CreateTable(
                name: "Nastavnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: false),
                    Prezime = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nastavnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Razlog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razlog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: false),
                    Prezime = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Konsultacije",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    NastavnikId = table.Column<int>(nullable: false),
                    DatumKonsultacija = table.Column<DateTime>(nullable: false),
                    RazlogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konsultacije", x => new { x.StudentId, x.NastavnikId, x.DatumKonsultacija });
                    table.ForeignKey(
                        name: "FK_Konsultacije_Datum",
                        column: x => x.DatumKonsultacija,
                        principalTable: "Datum",
                        principalColumn: "DatumKonsultacija",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konsultacije_Nastavnik",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Konsultacije_Razlog",
                        column: x => x.RazlogId,
                        principalTable: "Razlog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konsultacije_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Konsultacije_DatumKonsultacija",
                table: "Konsultacije",
                column: "DatumKonsultacija");

            migrationBuilder.CreateIndex(
                name: "IX_Konsultacije_NastavnikId",
                table: "Konsultacije",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Konsultacije_RazlogId",
                table: "Konsultacije",
                column: "RazlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Konsultacije");

            migrationBuilder.DropTable(
                name: "Datum");

            migrationBuilder.DropTable(
                name: "Nastavnik");

            migrationBuilder.DropTable(
                name: "Razlog");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}

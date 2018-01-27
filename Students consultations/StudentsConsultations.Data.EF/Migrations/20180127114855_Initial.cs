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
                    BrojRadneKnjizice = table.Column<string>(nullable: false),
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
                    RazlogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Opis = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razlog", x => x.RazlogId);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojIndeksa = table.Column<string>(nullable: false),
                    Ime = table.Column<string>(nullable: false),
                    Prezime = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrstaZadatak",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaZadatak", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ispit",
                columns: table => new
                {
                    RazlogId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ispit", x => x.RazlogId);
                    table.ForeignKey(
                        name: "FK_Ispit_Razlog_RazlogId",
                        column: x => x.RazlogId,
                        principalTable: "Razlog",
                        principalColumn: "RazlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projekat",
                columns: table => new
                {
                    RazlogId = table.Column<int>(nullable: false),
                    NazivIspita = table.Column<string>(nullable: false),
                    NazivProjekta = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekat", x => x.RazlogId);
                    table.ForeignKey(
                        name: "FK_Projekat_Razlog_RazlogId",
                        column: x => x.RazlogId,
                        principalTable: "Razlog",
                        principalColumn: "RazlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZavrsniRad",
                columns: table => new
                {
                    RazlogId = table.Column<int>(nullable: false),
                    Tip = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZavrsniRad", x => x.RazlogId);
                    table.ForeignKey(
                        name: "FK_ZavrsniRad_Razlog_RazlogId",
                        column: x => x.RazlogId,
                        principalTable: "Razlog",
                        principalColumn: "RazlogId",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "RazlogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konsultacije_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zadatak",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKonsultacija = table.Column<DateTime>(nullable: false),
                    NastavnikId = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    RokDoZavrsetka = table.Column<string>(nullable: true),
                    StudentId = table.Column<int>(nullable: false),
                    VrstaZadatkaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadatak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zadatak_Datum_DatumKonsultacija",
                        column: x => x.DatumKonsultacija,
                        principalTable: "Datum",
                        principalColumn: "DatumKonsultacija",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zadatak_Nastavnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zadatak_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zadatak_VrstaZadatak_VrstaZadatkaId",
                        column: x => x.VrstaZadatkaId,
                        principalTable: "VrstaZadatak",
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

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_DatumKonsultacija",
                table: "Zadatak",
                column: "DatumKonsultacija");

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_NastavnikId",
                table: "Zadatak",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_StudentId",
                table: "Zadatak",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_VrstaZadatkaId",
                table: "Zadatak",
                column: "VrstaZadatkaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ispit");

            migrationBuilder.DropTable(
                name: "Konsultacije");

            migrationBuilder.DropTable(
                name: "Projekat");

            migrationBuilder.DropTable(
                name: "Zadatak");

            migrationBuilder.DropTable(
                name: "ZavrsniRad");

            migrationBuilder.DropTable(
                name: "Datum");

            migrationBuilder.DropTable(
                name: "Nastavnik");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "VrstaZadatak");

            migrationBuilder.DropTable(
                name: "Razlog");
        }
    }
}

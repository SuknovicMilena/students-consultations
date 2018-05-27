using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentsConsultations.Data.EF.Migrations
{
    public partial class UpdatedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nastavnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojRadneKnjizice = table.Column<string>(nullable: false),
                    Ime = table.Column<string>(nullable: false),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true),
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
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrstaZadataka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaZadataka", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Konsultacija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DanUNedelji = table.Column<int>(nullable: false),
                    NastavnikId = table.Column<int>(nullable: false),
                    VremeDo = table.Column<DateTime>(nullable: false),
                    VremeOd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konsultacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Konsultacija_Nastavnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    NazivZavrsnogRada = table.Column<string>(nullable: true),
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
                        name: "FK_Zadatak_VrstaZadataka_VrstaZadatkaId",
                        column: x => x.VrstaZadatkaId,
                        principalTable: "VrstaZadataka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentKonsultacija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KonsultacijaId = table.Column<int>(nullable: false),
                    NastavnikId = table.Column<int>(nullable: false),
                    Odrzane = table.Column<bool>(nullable: false),
                    RazlogId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    VremeDo = table.Column<DateTime>(nullable: false),
                    VremeOd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentKonsultacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentKonsultacija_Konsultacija",
                        column: x => x.KonsultacijaId,
                        principalTable: "Konsultacija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentKonsultacija_Nastavnik",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StudentKonsultacija_Razlog",
                        column: x => x.RazlogId,
                        principalTable: "Razlog",
                        principalColumn: "RazlogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentKonsultacija_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Konsultacija_NastavnikId",
                table: "Konsultacija",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentKonsultacija_KonsultacijaId",
                table: "StudentKonsultacija",
                column: "KonsultacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentKonsultacija_NastavnikId",
                table: "StudentKonsultacija",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentKonsultacija_RazlogId",
                table: "StudentKonsultacija",
                column: "RazlogId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentKonsultacija_StudentId",
                table: "StudentKonsultacija",
                column: "StudentId");

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
                name: "Projekat");

            migrationBuilder.DropTable(
                name: "StudentKonsultacija");

            migrationBuilder.DropTable(
                name: "Zadatak");

            migrationBuilder.DropTable(
                name: "ZavrsniRad");

            migrationBuilder.DropTable(
                name: "Konsultacija");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "VrstaZadataka");

            migrationBuilder.DropTable(
                name: "Razlog");

            migrationBuilder.DropTable(
                name: "Nastavnik");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentsConsultations.Data.EF.Migrations
{
    public partial class Init : Migration
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
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    Ime = table.Column<string>(nullable: false),
                    Prezime = table.Column<string>(nullable: false),
                    BrojRadneKnjizice = table.Column<int>(nullable: true),
                    BrojIndeksa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Razlog",
                columns: table => new
                {
                    Naziv = table.Column<string>(nullable: true),
                    NazivIspita = table.Column<string>(nullable: true),
                    NazivProjekta = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    Tip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razlog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrsteZadataka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteZadataka", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Konsultacije",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    NastavnikId = table.Column<int>(nullable: false),
                    DatumKonsultacija = table.Column<DateTime>(nullable: false),
                    RazlogId = table.Column<int>(nullable: false),
                    Vreme = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konsultacije", x => new { x.StudentId, x.NastavnikId, x.DatumKonsultacija });
                    table.UniqueConstraint("AK_Konsultacije_DatumKonsultacija_NastavnikId_StudentId", x => new { x.DatumKonsultacija, x.NastavnikId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_Konsultacije_Datum",
                        column: x => x.DatumKonsultacija,
                        principalTable: "Datum",
                        principalColumn: "DatumKonsultacija",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konsultacije_Nastavnik",
                        column: x => x.NastavnikId,
                        principalTable: "Korisnici",
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
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zadaci",
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
                    table.PrimaryKey("PK_Zadaci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zadaci_Datum_DatumKonsultacija",
                        column: x => x.DatumKonsultacija,
                        principalTable: "Datum",
                        principalColumn: "DatumKonsultacija",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zadaci_Korisnici_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zadaci_Korisnici_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zadaci_VrsteZadataka_VrstaZadatkaId",
                        column: x => x.VrstaZadatkaId,
                        principalTable: "VrsteZadataka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Konsultacije_NastavnikId",
                table: "Konsultacije",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Konsultacije_RazlogId",
                table: "Konsultacije",
                column: "RazlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadaci_DatumKonsultacija",
                table: "Zadaci",
                column: "DatumKonsultacija");

            migrationBuilder.CreateIndex(
                name: "IX_Zadaci_NastavnikId",
                table: "Zadaci",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadaci_StudentId",
                table: "Zadaci",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadaci_VrstaZadatkaId",
                table: "Zadaci",
                column: "VrstaZadatkaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Konsultacije");

            migrationBuilder.DropTable(
                name: "Zadaci");

            migrationBuilder.DropTable(
                name: "Razlog");

            migrationBuilder.DropTable(
                name: "Datum");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "VrsteZadataka");
        }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using StudentsConsultations.Data.EF;
using System;

namespace StudentsConsultations.Data.EF.Migrations
{
    [DbContext(typeof(StudentskeKonsultacijeDbContext))]
    partial class StudentskeKonsultacijeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Datum", b =>
                {
                    b.Property<DateTime>("DatumKonsultacija");

                    b.HasKey("DatumKonsultacija");

                    b.ToTable("Datum");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Ispit", b =>
                {
                    b.Property<int>("RazlogId");

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.HasKey("RazlogId");

                    b.ToTable("Ispit");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Konsultacije", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("NastavnikId");

                    b.Property<DateTime>("DatumKonsultacija");

                    b.Property<bool>("Odrzane");

                    b.Property<int>("RazlogId");

                    b.HasKey("StudentId", "NastavnikId", "DatumKonsultacija");

                    b.HasIndex("DatumKonsultacija");

                    b.HasIndex("NastavnikId");

                    b.HasIndex("RazlogId");

                    b.ToTable("Konsultacije");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Nastavnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrojRadneKnjizice")
                        .IsRequired();

                    b.Property<string>("Ime")
                        .IsRequired();

                    b.Property<string>("Prezime")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Nastavnik");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Projekat", b =>
                {
                    b.Property<int>("RazlogId");

                    b.Property<string>("NazivIspita")
                        .IsRequired();

                    b.Property<string>("NazivProjekta")
                        .IsRequired();

                    b.HasKey("RazlogId");

                    b.ToTable("Projekat");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Razlog", b =>
                {
                    b.Property<int>("RazlogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Opis")
                        .IsRequired();

                    b.HasKey("RazlogId");

                    b.ToTable("Razlog");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrojIndeksa")
                        .IsRequired();

                    b.Property<string>("Ime")
                        .IsRequired();

                    b.Property<string>("Prezime")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.VrstaZadatka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("VrstaZadataka");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Zadatak", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumKonsultacija");

                    b.Property<int>("NastavnikId");

                    b.Property<string>("Opis");

                    b.Property<string>("RokDoZavrsetka");

                    b.Property<int>("StudentId");

                    b.Property<int>("VrstaZadatkaId");

                    b.HasKey("Id");

                    b.HasIndex("DatumKonsultacija");

                    b.HasIndex("NastavnikId");

                    b.HasIndex("StudentId");

                    b.HasIndex("VrstaZadatkaId");

                    b.ToTable("Zadatak");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.ZavrsniRad", b =>
                {
                    b.Property<int>("RazlogId");

                    b.Property<string>("NazivZavrsnogRada");

                    b.Property<string>("Tip")
                        .IsRequired();

                    b.HasKey("RazlogId");

                    b.ToTable("ZavrsniRad");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Ispit", b =>
                {
                    b.HasOne("StudentsConsultations.Data.Domain.Razlog", "Razlog")
                        .WithOne("Ispit")
                        .HasForeignKey("StudentsConsultations.Data.Domain.Ispit", "RazlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Konsultacije", b =>
                {
                    b.HasOne("StudentsConsultations.Data.Domain.Datum", "DatumObjekat")
                        .WithMany("Konsultacije")
                        .HasForeignKey("DatumKonsultacija")
                        .HasConstraintName("FK_Konsultacije_Datum");

                    b.HasOne("StudentsConsultations.Data.Domain.Nastavnik", "Nastavnik")
                        .WithMany("Konsultacije")
                        .HasForeignKey("NastavnikId")
                        .HasConstraintName("FK_Konsultacije_Nastavnik")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentsConsultations.Data.Domain.Razlog", "Razlog")
                        .WithMany("Konsultacije")
                        .HasForeignKey("RazlogId")
                        .HasConstraintName("FK_Konsultacije_Razlog");

                    b.HasOne("StudentsConsultations.Data.Domain.Student", "Student")
                        .WithMany("Konsultacije")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_Konsultacije_Student");
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Projekat", b =>
                {
                    b.HasOne("StudentsConsultations.Data.Domain.Razlog", "Razlog")
                        .WithOne("Projekat")
                        .HasForeignKey("StudentsConsultations.Data.Domain.Projekat", "RazlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.Zadatak", b =>
                {
                    b.HasOne("StudentsConsultations.Data.Domain.Datum", "Datum")
                        .WithMany()
                        .HasForeignKey("DatumKonsultacija")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentsConsultations.Data.Domain.Nastavnik", "Nastavnik")
                        .WithMany()
                        .HasForeignKey("NastavnikId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentsConsultations.Data.Domain.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentsConsultations.Data.Domain.VrstaZadatka", "VrstaZadatka")
                        .WithMany("Zadaci")
                        .HasForeignKey("VrstaZadatkaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudentsConsultations.Data.Domain.ZavrsniRad", b =>
                {
                    b.HasOne("StudentsConsultations.Data.Domain.Razlog", "Razlog")
                        .WithOne("ZavrsniRad")
                        .HasForeignKey("StudentsConsultations.Data.Domain.ZavrsniRad", "RazlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudentsConsultations.Data.EF
{
    public partial class StudentskeKonsultacijeDbContext : DbContext
    {
        public virtual DbSet<Datum> Datum { get; set; }
        public virtual DbSet<Konsultacije> Konsultacije { get; set; }
        public virtual DbSet<Nastavnik> Nastavnik { get; set; }
        public virtual DbSet<Razlog> Razlog { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        public StudentskeKonsultacijeDbContext(DbContextOptions<StudentskeKonsultacijeDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Konsultacije>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.NastavnikId, e.DatumKonsultacija });

                entity.HasOne(d => d.DatumObjekat)
                    .WithMany(p => p.Konsultacije)
                    .HasForeignKey(d => d.DatumKonsultacija)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Konsultacije_Datum");

                entity.HasOne(d => d.Nastavnik)
                    .WithMany(p => p.Konsultacije)
                    .HasForeignKey(d => d.NastavnikId)
                    .HasConstraintName("FK_Konsultacije_Nastavnik");

                entity.HasOne(d => d.Razlog)
                    .WithMany(p => p.Konsultacije)
                    .HasForeignKey(d => d.RazlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Konsultacije_Razlog");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Konsultacije)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Konsultacije_Student");
            });
        }
    }
}

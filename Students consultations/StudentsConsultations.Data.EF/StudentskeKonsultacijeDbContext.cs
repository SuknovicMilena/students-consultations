using Microsoft.EntityFrameworkCore;
using StudentsConsultations.Data.Domain;

namespace StudentsConsultations.Data.EF
{
    public partial class StudentskeKonsultacijeDbContext : DbContext
    {
        public virtual DbSet<StudentKonsultacija> StudentKonsultacija { get; set; }
        public virtual DbSet<Konsultacija> Konsultacija { get; set; }
        public virtual DbSet<Nastavnik> Nastavnik { get; set; }
        public virtual DbSet<Razlog> Razlog { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Zadatak> Zadatak { get; set; }
        public virtual DbSet<VrstaZadatka> VrstaZadataka { get; set; }
        public virtual DbSet<Ispit> Ispit { get; set; }
        public virtual DbSet<ZavrsniRad> ZavrsniRad { get; set; }
        public virtual DbSet<Projekat> Projekat { get; set; }


        public StudentskeKonsultacijeDbContext(DbContextOptions<StudentskeKonsultacijeDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StudentKonsultacija>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.HasOne(d => d.Nastavnik)
                    .WithMany(p => p.StudentKonsultacija)
                    .HasForeignKey(d => d.NastavnikId)
                    .HasConstraintName("FK_StudentKonsultacija_Nastavnik");

                entity.HasOne(d => d.Razlog)
                    .WithMany(p => p.StudentKonsultacija)
                    .HasForeignKey(d => d.RazlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentKonsultacija_Razlog");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentKonsultacija)
                    .HasForeignKey(d => d.StudentId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentKonsultacija_Student");

                entity.HasOne(d => d.Konsultacija)
                   .WithMany(p => p.StudentKonsultacija)
                   .HasForeignKey(d => d.KonsultacijaId)
                   .HasConstraintName("FK_StudentKonsultacija_Konsultacija");
            });
        }
    }
}

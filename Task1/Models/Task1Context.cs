using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Task1.Models
{
    public partial class Task1Context : DbContext
    {
        public Task1Context()
        {
        }

        public Task1Context(DbContextOptions<Task1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Osoba> Osoby { get; set; }
        public virtual DbSet<Samochod> Samochody { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-PUG43O1V;Database=Task1;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Osoba>(entity =>
            {
                entity.ToTable("OSOBA");

                entity.Property(e => e.OsobaId)
                    .ValueGeneratedNever()
                    .HasColumnName("osoba_id");

                entity.Property(e => e.DataProd)
                    .HasColumnType("date")
                    .HasColumnName("data_prod");

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("imie");

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("nazwisko");

                entity.Property(e => e.SamochodId).HasColumnName("samochod_id");

                entity.HasOne(d => d.Samochod)
                    .WithMany(p => p.Osoby)
                    .HasForeignKey(d => d.SamochodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OSOBY__samochod___2B3F6F97");
            });

            modelBuilder.Entity<Samochod>(entity =>
            {
                entity.ToTable("SAMOCHOD");

                entity.Property(e => e.SamochodId)
                    .ValueGeneratedNever()
                    .HasColumnName("samochod_id");

                entity.Property(e => e.Cena)
                    .HasColumnType("money")
                    .HasColumnName("cena");

                entity.Property(e => e.Pojemnosc)
                    .HasColumnType("numeric(5, 3)")
                    .HasColumnName("pojemnosc");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

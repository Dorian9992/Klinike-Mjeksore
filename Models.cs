
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KlinikeMjekesore
{
    public enum Roli
    {
        Administrator,
        Mjek,
        Recepsionist
    }

    public class Perdorues
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string Email { get; set; }
        public string FjalekalimiHash { get; set; }
        public Roli Roli { get; set; }
    }

    public class Pacient
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string NrPersonal { get; set; }
        public DateTime DataLindjes { get; set; }
        public List<Takim> Takimet { get; set; }
    }

    public class Mjek
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string Specializimi { get; set; }
        public List<Takim> Takimet { get; set; }
    }

    public class Takim
    {
        public int Id { get; set; }
        public DateTime DataOra { get; set; }
        public string Diagnoza { get; set; }

        public int PacientId { get; set; }
        public Pacient Pacient { get; set; }

        public int MjekId { get; set; }
        public Mjek Mjek { get; set; }
    }

    public class KlinikaDbContext : DbContext
    {
        public DbSet<Perdorues> Perdoruesit { get; set; }
        public DbSet<Pacient> Pacientet { get; set; }
        public DbSet<Mjek> Mjeket { get; set; }
        public DbSet<Takim> Takimet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=klinika.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Takim>()
                .HasOne(t => t.Pacient)
                .WithMany(p => p.Takimet)
                .HasForeignKey(t => t.PacientId);

            modelBuilder.Entity<Takim>()
                .HasOne(t => t.Mjek)
                .WithMany(m => m.Takimet)
                .HasForeignKey(t => t.MjekId);
        }
    }
}

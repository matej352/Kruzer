using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KruzerApp.Models;

public partial class KruzerContext : DbContext
{
    public KruzerContext()
    {
    }

    public KruzerContext(DbContextOptions<KruzerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Krstarenje> Krstarenjes { get; set; }

    public virtual DbSet<Lokacija> Lokacijas { get; set; }

    public virtual DbSet<Odgovor> Odgovors { get; set; }

    public virtual DbSet<Putnik> Putniks { get; set; }

    public virtual DbSet<Rezervacija> Rezervacijas { get; set; }

    public virtual DbSet<Upit> Upits { get; set; }

    public virtual DbSet<Zaposlenik> Zaposleniks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Kruzer;Username=postgres;Password=Bazepodataka1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("administrator_pkey");

            entity.ToTable("administrator");

            entity.HasIndex(e => e.Nadimak, "administrator_nadimak_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Ime)
                .HasMaxLength(50)
                .HasColumnName("ime");
            entity.Property(e => e.Lozinka)
                .HasMaxLength(1000)
                .HasColumnName("lozinka");
            entity.Property(e => e.Nadimak)
                .HasMaxLength(50)
                .HasColumnName("nadimak");
            entity.Property(e => e.Prezime)
                .HasMaxLength(50)
                .HasColumnName("prezime");
        });

        modelBuilder.Entity<Krstarenje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("krstarenje_pkey");

            entity.ToTable("krstarenje");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Datumkraj).HasColumnName("datumkraj");
            entity.Property(e => e.Datumpocetak).HasColumnName("datumpocetak");
            entity.Property(e => e.Kapacitet).HasColumnName("kapacitet");
            entity.Property(e => e.Naslov)
                .HasMaxLength(100)
                .HasColumnName("naslov");
            entity.Property(e => e.Opis)
                .HasMaxLength(2000)
                .HasColumnName("opis");
            entity.Property(e => e.Popunjenost).HasColumnName("popunjenost");

            entity.HasOne(d => d.Admin).WithMany(p => p.Krstarenjes)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("krstarenje_admin_id_fkey");

            entity.HasMany(d => d.Lokacijas).WithMany(p => p.Krstarenjes)
                .UsingEntity<Dictionary<string, object>>(
                    "Posjećuje",
                    r => r.HasOne<Lokacija>().WithMany()
                        .HasForeignKey("LokacijaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("posjećuje_lokacija_id_fkey"),
                    l => l.HasOne<Krstarenje>().WithMany()
                        .HasForeignKey("KrstarenjeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("posjećuje_krstarenje_id_fkey"),
                    j =>
                    {
                        j.HasKey("KrstarenjeId", "LokacijaId").HasName("posjećuje_pkey");
                        j.ToTable("posjećuje");
                        j.IndexerProperty<int>("KrstarenjeId").HasColumnName("krstarenje_id");
                        j.IndexerProperty<int>("LokacijaId").HasColumnName("lokacija_id");
                    });
        });

        modelBuilder.Entity<Lokacija>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lokacija_pkey");

            entity.ToTable("lokacija");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Država)
                .HasMaxLength(200)
                .HasColumnName("država");
            entity.Property(e => e.Grad)
                .HasMaxLength(200)
                .HasColumnName("grad");
        });

        modelBuilder.Entity<Odgovor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("odgovor_pkey");

            entity.ToTable("odgovor");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Sadržaj)
                .HasMaxLength(2000)
                .HasColumnName("sadržaj");
            entity.Property(e => e.UpitId).HasColumnName("upit_id");
            entity.Property(e => e.Vrijeme).HasColumnName("vrijeme");
            entity.Property(e => e.ZaposlenikId).HasColumnName("zaposlenik_id");

            entity.HasOne(d => d.Upit).WithMany(p => p.Odgovors)
                .HasForeignKey(d => d.UpitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("odgovor_upit_id_fkey");

            entity.HasOne(d => d.Zaposlenik).WithMany(p => p.Odgovors)
                .HasForeignKey(d => d.ZaposlenikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("odgovor_zaposlenik_id_fkey");
        });

        modelBuilder.Entity<Putnik>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("putnik_pkey");

            entity.ToTable("putnik");

            entity.HasIndex(e => e.Nadimak, "putnik_nadimak_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Ime)
                .HasMaxLength(50)
                .HasColumnName("ime");
            entity.Property(e => e.Lozinka)
                .HasMaxLength(1000)
                .HasColumnName("lozinka");
            entity.Property(e => e.Nadimak)
                .HasMaxLength(50)
                .HasColumnName("nadimak");
            entity.Property(e => e.Prezime)
                .HasMaxLength(50)
                .HasColumnName("prezime");
            entity.Property(e => e.Spol)
                .HasMaxLength(1)
                .HasColumnName("spol");
        });

        modelBuilder.Entity<Rezervacija>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rezervacija_pkey");

            entity.ToTable("rezervacija");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Brojputnika).HasColumnName("brojputnika");
            entity.Property(e => e.KrstarenjeId).HasColumnName("krstarenje_id");
            entity.Property(e => e.PutnikId).HasColumnName("putnik_id");
            entity.Property(e => e.Vrijeme).HasColumnName("vrijeme");

            entity.HasOne(d => d.Krstarenje).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.KrstarenjeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rezervacija_krstarenje_id_fkey");

            entity.HasOne(d => d.Putnik).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.PutnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rezervacija_putnik_id_fkey");
        });

        modelBuilder.Entity<Upit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("upit_pkey");

            entity.ToTable("upit");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.KrstarenjeId).HasColumnName("krstarenje_id");
            entity.Property(e => e.PutnikId).HasColumnName("putnik_id");
            entity.Property(e => e.Sadržaj)
                .HasMaxLength(2000)
                .HasColumnName("sadržaj");
            entity.Property(e => e.Vrijeme).HasColumnName("vrijeme");

            entity.HasOne(d => d.Krstarenje).WithMany(p => p.Upits)
                .HasForeignKey(d => d.KrstarenjeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("upit_krstarenje_id_fkey");

            entity.HasOne(d => d.Putnik).WithMany(p => p.Upits)
                .HasForeignKey(d => d.PutnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("upit_putnik_id_fkey");
        });

        modelBuilder.Entity<Zaposlenik>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("zaposlenik_pkey");

            entity.ToTable("zaposlenik");

            entity.HasIndex(e => e.Nadimak, "zaposlenik_nadimak_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Ime)
                .HasMaxLength(50)
                .HasColumnName("ime");
            entity.Property(e => e.Lozinka)
                .HasMaxLength(1000)
                .HasColumnName("lozinka");
            entity.Property(e => e.Nadimak)
                .HasMaxLength(50)
                .HasColumnName("nadimak");
            entity.Property(e => e.Oib)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("oib");
            entity.Property(e => e.Prezime)
                .HasMaxLength(50)
                .HasColumnName("prezime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

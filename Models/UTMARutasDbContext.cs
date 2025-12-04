using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UTMA_ROUTES_API.Models;

public partial class UTMARutasDbContext : DbContext
{
    public UTMARutasDbContext(DbContextOptions<UTMARutasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatUsuario> CatUsuarios { get; set; }

    public virtual DbSet<Cathorario> Cathorarios { get; set; }

    public virtual DbSet<Catparada> Catparadas { get; set; }

    public virtual DbSet<Catruta> Catrutas { get; set; }

    public virtual DbSet<Catzona> Catzonas { get; set; }

    public virtual DbSet<Relrutasparada> Relrutasparadas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<CatUsuario>(entity =>
        {
            entity.HasKey(e => e.ECodUsuario).HasName("PRIMARY");

            entity.ToTable("cat_usuarios");

            entity.Property(e => e.ECodUsuario)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("eCodUsuario");
            entity.Property(e => e.FhFechaActualizacion)
                .HasColumnType("datetime")
                .HasColumnName("fhFechaActualizacion");
            entity.Property(e => e.FhFechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fhFechaRegistro");
            entity.Property(e => e.TCodEstatus)
                .HasMaxLength(2)
                .HasColumnName("tCodEstatus");
            entity.Property(e => e.TCorreo)
                .HasMaxLength(200)
                .HasColumnName("tCorreo");
            entity.Property(e => e.TImagen)
                .HasMaxLength(500)
                .HasColumnName("tImagen");
            entity.Property(e => e.TNombre)
                .HasMaxLength(200)
                .HasColumnName("tNombre");
            entity.Property(e => e.TPassword)
                .HasMaxLength(255)
                .HasColumnName("tPassword");
            entity.Property(e => e.TPuesto)
                .HasMaxLength(200)
                .HasColumnName("tPuesto");
            entity.Property(e => e.TUsuario)
                .HasMaxLength(50)
                .HasColumnName("tUsuario");
        });

        modelBuilder.Entity<Cathorario>(entity =>
        {
            entity.HasKey(e => e.ECodHorario).HasName("PRIMARY");

            entity.ToTable("cathorarios");

            entity.HasIndex(e => e.ECodRuta, "FKCatHorariosCatRutas00");

            entity.Property(e => e.ECodHorario)
                .HasColumnType("int(11)")
                .HasColumnName("eCodHorario");
            entity.Property(e => e.ECodRuta)
                .HasColumnType("int(11)")
                .HasColumnName("eCodRuta");
            entity.Property(e => e.TmHoraEntrada)
                .HasColumnType("time")
                .HasColumnName("tmHoraEntrada");
            entity.Property(e => e.TmHoraSalida)
                .HasColumnType("time")
                .HasColumnName("tmHoraSalida");

            entity.HasOne(d => d.ECodRutaNavigation).WithMany(p => p.Cathorarios)
                .HasForeignKey(d => d.ECodRuta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCatHorariosCatRutas00");
        });

        modelBuilder.Entity<Catparada>(entity =>
        {
            entity.HasKey(e => e.ECodParada).HasName("PRIMARY");

            entity.ToTable("catparadas");

            entity.HasIndex(e => e.ECodRuta, "FKCatParadasCatRutas00");

            entity.Property(e => e.ECodParada)
                .HasColumnType("int(11)")
                .HasColumnName("eCodParada");
            entity.Property(e => e.DLatitud)
                .HasPrecision(10, 6)
                .HasColumnName("dLatitud");
            entity.Property(e => e.DLongitud)
                .HasPrecision(10, 6)
                .HasColumnName("dLongitud");
            entity.Property(e => e.ECodRuta)
                .HasColumnType("int(11)")
                .HasColumnName("eCodRuta");
            entity.Property(e => e.TNombre)
                .HasMaxLength(100)
                .HasColumnName("tNombre");

            entity.HasOne(d => d.ECodRutaNavigation).WithMany(p => p.Catparada)
                .HasForeignKey(d => d.ECodRuta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCatParadasCatRutas00");
        });

        modelBuilder.Entity<Catruta>(entity =>
        {
            entity.HasKey(e => e.ECodRuta).HasName("PRIMARY");

            entity.ToTable("catrutas");

            entity.HasIndex(e => e.ECodZona, "FKCatRutasCatZonas00");

            entity.Property(e => e.ECodRuta)
                .HasColumnType("int(11)")
                .HasColumnName("eCodRuta");
            entity.Property(e => e.ECodZona)
                .HasColumnType("int(11)")
                .HasColumnName("eCodZona");
            entity.Property(e => e.ENumero)
                .HasColumnType("int(11)")
                .HasColumnName("eNumero");
            entity.Property(e => e.TDescripcion)
                .HasMaxLength(64)
                .HasColumnName("tDescripcion");
            entity.Property(e => e.TNombre)
                .HasMaxLength(32)
                .HasColumnName("tNombre");

            entity.HasOne(d => d.ECodZonaNavigation).WithMany(p => p.Catruta)
                .HasForeignKey(d => d.ECodZona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCatRutasCatZonas00");
        });

        modelBuilder.Entity<Catzona>(entity =>
        {
            entity.HasKey(e => e.ECodZona).HasName("PRIMARY");

            entity.ToTable("catzonas");

            entity.Property(e => e.ECodZona)
                .HasColumnType("int(11)")
                .HasColumnName("eCodZona");
            entity.Property(e => e.TDescripcion)
                .HasMaxLength(128)
                .HasColumnName("tDescripcion");
            entity.Property(e => e.TNombre)
                .HasMaxLength(64)
                .HasColumnName("tNombre");
        });

        modelBuilder.Entity<Relrutasparada>(entity =>
        {
            entity.HasKey(e => e.ECodRutasParadas).HasName("PRIMARY");

            entity.ToTable("relrutasparadas");

            entity.HasIndex(e => e.ECodParada, "FKRutasParadasCatParadas00");

            entity.HasIndex(e => e.ECodRuta, "FKRutasParadasCatRutas00");

            entity.Property(e => e.ECodRutasParadas)
                .HasColumnType("int(11)")
                .HasColumnName("eCodRutasParadas");
            entity.Property(e => e.ECodParada)
                .HasColumnType("int(11)")
                .HasColumnName("eCodParada");
            entity.Property(e => e.ECodRuta)
                .HasColumnType("int(11)")
                .HasColumnName("eCodRuta");

            entity.HasOne(d => d.ECodParadaNavigation).WithMany(p => p.Relrutasparada)
                .HasForeignKey(d => d.ECodParada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRutasParadasCatParadas00");

            entity.HasOne(d => d.ECodRutaNavigation).WithMany(p => p.Relrutasparada)
                .HasForeignKey(d => d.ECodRuta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRutasParadasCatRutas00");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

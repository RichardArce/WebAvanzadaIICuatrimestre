using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAvanzadaIICuatrimestre.DAL.Entidades;

namespace WebAvanzadaIICuatrimestre.DAL.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carro> Carros { get; set; }

    public virtual DbSet<Duenno> Duennos { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // Intentionally left blank. Configure the provider externally.
        {
            if (!optionsBuilder.IsConfigured)
            {
                // No-op: connection configured via DI in application.
            }
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carro>(entity =>
        {
            entity.ToTable("Carro");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Marca).HasDefaultValue("Sin Marca");
            entity.Property(e => e.ValorFiscal).HasColumnType("NUMERIC");
            entity.Property(e => e.Fkduenno).HasColumnName("FKDUENNO");

            entity.HasOne(d => d.FkduennoNavigation).WithMany(p => p.Carros).HasForeignKey(d => d.Fkduenno);
        });

        modelBuilder.Entity<Duenno>(entity =>
        {
            entity.ToTable("Duenno");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido2).HasDefaultValue("Sin apellido");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.ToTable("Telefono");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fkduenno).HasColumnName("FKDUENNO");

            entity.HasOne(d => d.FkduennoNavigation).WithMany(p => p.Telefonos).HasForeignKey(d => d.Fkduenno);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // If the context isn't configured by the caller (e.g. design-time tools),
        // do not configure a provider here. The application registers the provider
        // via dependency injection in Program.cs. This avoids hardcoding the
        // connection string in the generated context.
        if (!optionsBuilder.IsConfigured)
        {
            // Intentionally left blank. Configure the context externally.
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carro>(entity =>
        {
            entity.ToTable("CARRO");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Fkduenno).HasColumnName("FKDUENNO");
            entity.Property(e => e.Marca).HasDefaultValue("Sin Marca");
            entity.Property(e => e.ValorFiscal).HasColumnType("NUMERIC");

            entity.HasOne(d => d.FkduennoNavigation).WithMany(p => p.Carros).HasForeignKey(d => d.Fkduenno);
        });

        modelBuilder.Entity<Duenno>(entity =>
        {
            entity.ToTable("DUENNO");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Apellido2).HasDefaultValue("Sin apellido");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

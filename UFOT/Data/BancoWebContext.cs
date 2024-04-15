using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UFOT.Models;

namespace UFOT.Data;

public partial class BancoWebContext : DbContext
{
    public BancoWebContext()
    {
    }

    public BancoWebContext(DbContextOptions<BancoWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banco> Bancos { get; set; }

    public virtual DbSet<Beneficiario> Beneficiarios { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=bancoweb.czqg8a8oopxh.us-east-2.rds.amazonaws.com, 1433;Initial Catalog=Banco_Web;Integrated Security=false; Encrypt=false; User ID=admin; Password=12345678");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.BancoId).HasName("PK__Bancos__4A8BAC1500A8F36C");

            entity.Property(e => e.BancoId).HasColumnName("BancoID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Beneficiario>(entity =>
        {
            entity.HasKey(e => e.BeneficiarioId).HasName("PK__Benefici__5A04A8D3C22EE431");

            entity.Property(e => e.BeneficiarioId).HasColumnName("BeneficiarioID");
            entity.Property(e => e.BancoId).HasColumnName("BancoID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.NumeroCuenta).HasMaxLength(20);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Banco).WithMany(p => p.Beneficiarios)
                .HasForeignKey(d => d.BancoId)
                .HasConstraintName("FK__Beneficia__Banco__3D5E1FD2");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Beneficiarios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Beneficiarios_Usuarios");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Log__5E5499A8B6585386");

            entity.ToTable("Log");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.TipoMensaje).HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE79834850C50");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Documento, "UQ__Usuario__AF73706D7A0357ED").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Clave).HasMaxLength(32);
            entity.Property(e => e.Documento).HasMaxLength(16);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.NombreUsuario).HasMaxLength(16);
            entity.Property(e => e.Rol)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Telefono).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

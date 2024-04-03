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

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<PagosPrestamo> PagosPrestamos { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<TransaccionBeneficiario> TransaccionBeneficiarios { get; set; }

    public virtual DbSet<TransaccionCuentum> TransaccionCuenta { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DRAGONDEOZ;initial catalog=Banco_Web; trusted_connection=true; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.BancoId).HasName("PK__Bancos__4A8BAC1572F48E23");
        });

        modelBuilder.Entity<Beneficiario>(entity =>
        {
            entity.HasKey(e => e.BeneficiarioId).HasName("PK__Benefici__5A04A8D38F9F78E9");

            entity.HasOne(d => d.Banco).WithMany(p => p.Beneficiarios).HasConstraintName("FK__Beneficia__Banco__4E88ABD4");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.NumeroCuenta).HasName("PK__Cuentas__E039507AA81203CC");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Cuenta).HasConstraintName("FK__Cuentas__Usuario__3A81B327");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.MovimientoId).HasName("PK__Movimien__BF923FCC9E30D151");

            entity.HasOne(d => d.CuentaDestino).WithMany(p => p.MovimientoCuentaDestinos).HasConstraintName("FK__Movimient__Cuent__49C3F6B7");

            entity.HasOne(d => d.CuentaOrigen).WithMany(p => p.MovimientoCuentaOrigens).HasConstraintName("FK__Movimient__Cuent__48CFD27E");

            entity.HasOne(d => d.Prestamo).WithMany(p => p.Movimientos).HasConstraintName("FK__Movimient__Prest__47DBAE45");
        });

        modelBuilder.Entity<PagosPrestamo>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__PagosPre__F00B6158FB8A53CF");

            entity.HasOne(d => d.NumeroCuentaNavigation).WithMany(p => p.PagosPrestamos).HasConstraintName("FK__PagosPres__Numer__571DF1D5");

            entity.HasOne(d => d.Prestamo).WithMany(p => p.PagosPrestamos).HasConstraintName("FK__PagosPres__Prest__5629CD9C");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.PrestamoId).HasName("PK__Prestamo__AA58A080F0401DC9");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Prestamos).HasConstraintName("FK__Prestamos__Usuar__44FF419A");
        });

        modelBuilder.Entity<TransaccionBeneficiario>(entity =>
        {
            entity.HasKey(e => e.TransaccionId).HasName("PK__Transacc__86A849DE2B4C1AFA");

            entity.Property(e => e.TransaccionId).ValueGeneratedNever();

            entity.HasOne(d => d.Beneficiario).WithMany(p => p.TransaccionBeneficiarios).HasConstraintName("FK__Transacci__Benef__534D60F1");

            entity.HasOne(d => d.CuentaOrigenNavigation).WithMany(p => p.TransaccionBeneficiarios).HasConstraintName("FK__Transacci__Cuent__5165187F");

            entity.HasOne(d => d.Transaccion).WithOne(p => p.TransaccionBeneficiario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Trans__52593CB8");
        });

        modelBuilder.Entity<TransaccionCuentum>(entity =>
        {
            entity.HasKey(e => e.TransaccionId).HasName("PK__Transacc__86A849DEC0951D2A");

            entity.Property(e => e.TransaccionId).ValueGeneratedNever();

            entity.HasOne(d => d.CuentaOrigenNavigation).WithMany(p => p.TransaccionCuentumCuentaOrigenNavigations).HasConstraintName("FK__Transacci__Cuent__403A8C7D");

            entity.HasOne(d => d.NumeroCuentaNavigation).WithMany(p => p.TransaccionCuentumNumeroCuentaNavigations).HasConstraintName("FK__Transacci__Numer__4222D4EF");

            entity.HasOne(d => d.Transaccion).WithOne(p => p.TransaccionCuentum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Trans__412EB0B6");
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.TransaccionId).HasName("PK__Transacc__86A849DEF1FB4EAA");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Transacciones).HasConstraintName("FK__Transacci__Monto__3D5E1FD2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE79883DD3D72");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

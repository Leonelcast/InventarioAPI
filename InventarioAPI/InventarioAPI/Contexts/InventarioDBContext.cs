using InventarioAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Contexts
{
    public class InventarioDBContext: DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<TipoEmpaque> TipoEmpaques { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public DbSet<EmailCliente> EmailClientes { get; set; }
        public DbSet<EmailProveedor> EmailProveedores { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<TelefonoCliente> TelefonoClientes { get; set; }
        public DbSet<TelefonoProveedor> TelefonoProveedores { get; set; }


        public InventarioDBContext(DbContextOptions<InventarioDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Categorias
            modelBuilder.Entity<Categoria>().ToTable("Categorias")
                .HasKey(key => key.CodigoCategoria);
            base.OnModelCreating(modelBuilder);
            //tipoEmpaque
            modelBuilder.Entity<TipoEmpaque>().ToTable("TipoEmpaque")
                .HasKey(key => key.CodigoEmpaque);
            base.OnModelCreating(modelBuilder);
            //Producto
            modelBuilder.Entity<Producto>().ToTable("Productos")
                .HasKey(key => key.CodigoProducto);
            base.OnModelCreating(modelBuilder);
            //Inventario
            modelBuilder.Entity<Inventario>().ToTable("Inventarios")
                .HasKey(key => key.CodigoInventario);
            base.OnModelCreating(modelBuilder);
            //DetalleCompra
            modelBuilder.Entity<DetalleCompra>().ToTable("DetalleCompras")
                .HasKey(key => key.IdDetalle);
            base.OnModelCreating(modelBuilder);
            //Detallfactura
            modelBuilder.Entity<DetalleFactura>().ToTable("DetalleFacturas")
                .HasKey(key => key.CodigoDetalle);
            base.OnModelCreating(modelBuilder);
            //Compra
            modelBuilder.Entity<Compra>().ToTable("Compras")
                .HasKey(key => key.IdCompra);
            base.OnModelCreating(modelBuilder);
            //Factura
            modelBuilder.Entity<Factura>().ToTable("Facturas")
                .HasKey(key => key.NumeroFactura);
            base.OnModelCreating(modelBuilder);
            //Clientes
            modelBuilder.Entity<Cliente>().ToTable("Clientes")
                .HasKey(key => key.Nit);
            base.OnModelCreating(modelBuilder);
            //proveedor
            modelBuilder.Entity<Proveedor>().ToTable("Proveedores")
                .HasKey(key => key.CodigoProveedor);
            base.OnModelCreating(modelBuilder);
            //TelefonoProveedor
            modelBuilder.Entity<TelefonoProveedor>().ToTable("TelefonoProveedores")
               .HasKey(key => key.CodigoTelefono);
            base.OnModelCreating(modelBuilder);
            //EmailProveedor
            modelBuilder.Entity<EmailProveedor>().ToTable("EmailProveedores")
               .HasKey(key => key.CodigoEmail);
            base.OnModelCreating(modelBuilder);
            //EmailCliente
            modelBuilder.Entity<EmailCliente>().ToTable("EmailClientes")
            .HasKey(key => key.CodigoEmail);
            base.OnModelCreating(modelBuilder);
            //telefonoCliente
            modelBuilder.Entity<TelefonoCliente>().ToTable("TelefonoCliente")
         .HasKey(key => key.CodigoTelefono);
            base.OnModelCreating(modelBuilder);

        }
    }
}

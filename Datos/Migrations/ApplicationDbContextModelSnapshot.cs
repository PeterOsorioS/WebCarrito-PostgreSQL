﻿// <auto-generated />
using System;
using AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Datos.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entidad.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCategoria"));

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RutaImagen")
                        .HasColumnType("text");

                    b.HasKey("IdCategoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Entidad.Departamento", b =>
                {
                    b.Property<string>("IdDepartamento")
                        .HasColumnType("text");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.HasKey("IdDepartamento");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("Entidad.Detalle_Venta", b =>
                {
                    b.Property<int>("IdDetalleVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdDetalleVenta"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<int>("IdProducto")
                        .HasColumnType("integer");

                    b.Property<int>("IdVenta")
                        .HasColumnType("integer");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("IdDetalleVenta");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdVenta");

                    b.ToTable("Detalle_Ventas");
                });

            modelBuilder.Entity("Entidad.Marca", b =>
                {
                    b.Property<int>("IdMarca")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdMarca"));

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("IdMarca");

                    b.ToTable("Marcas");
                });

            modelBuilder.Entity("Entidad.Municipio", b =>
                {
                    b.Property<int>("IdMunicipio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdMunicipio"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("IdDepartamento")
                        .HasColumnType("text");

                    b.HasKey("IdMunicipio");

                    b.ToTable("Municipio");
                });

            modelBuilder.Entity("Entidad.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdProducto"));

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("integer");

                    b.Property<int>("IdMarca")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NombreImagen")
                        .HasColumnType("text");

                    b.Property<decimal>("Precio")
                        .HasColumnType("numeric");

                    b.Property<string>("RutaImagen")
                        .HasColumnType("text");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.HasKey("IdProducto");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdMarca");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Entidad.Rol", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdRol"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.HasKey("IdRol");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Entidad.Slider", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RutaImagen")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("Entidad.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdUsuario"));

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("IdRol")
                        .HasColumnType("integer");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdUsuario");

                    b.HasIndex("Correo")
                        .IsUnique();

                    b.HasIndex("IdRol");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Entidad.Venta", b =>
                {
                    b.Property<int>("IdVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdVenta"));

                    b.Property<string>("Contacto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("FechaVenta")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer");

                    b.Property<decimal>("MontoTotal")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("TotalProducto")
                        .HasColumnType("integer");

                    b.HasKey("IdVenta");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("Entidad.Detalle_Venta", b =>
                {
                    b.HasOne("Entidad.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entidad.Venta", "Venta")
                        .WithMany("DetalleVenta")
                        .HasForeignKey("IdVenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("Entidad.Producto", b =>
                {
                    b.HasOne("Entidad.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entidad.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("IdMarca")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("Entidad.Usuario", b =>
                {
                    b.HasOne("Entidad.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Entidad.Venta", b =>
                {
                    b.HasOne("Entidad.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Entidad.Venta", b =>
                {
                    b.Navigation("DetalleVenta");
                });
#pragma warning restore 612, 618
        }
    }
}

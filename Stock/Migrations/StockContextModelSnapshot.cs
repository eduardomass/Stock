﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Stock.Migrations
{
    [DbContext(typeof(StockContext))]
    partial class StockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Stock.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("Stock.Models.JornadaLaboral", b =>
                {
                    b.Property<int>("JornadaLaboralId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaYHora")
                        .HasColumnType("TEXT");

                    b.Property<int>("TrabajadorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("JornadaLaboralId");

                    b.HasIndex("TrabajadorId");

                    b.ToTable("JornadasLaborales");
                });

            modelBuilder.Entity("Stock.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Stock.Models.ProductoPorUsuario", b =>
                {
                    b.Property<int>("ProductoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductoId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ProductosPorUsuario");
                });

            modelBuilder.Entity("Stock.Models.Trabajador", b =>
                {
                    b.Property<int>("TrabajadorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NombreYApllido")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TrabajadorId");

                    b.ToTable("Trabajadores");
                });

            modelBuilder.Entity("Stock.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Stock.Models.JornadaLaboral", b =>
                {
                    b.HasOne("Stock.Models.Trabajador", "Trabajador")
                        .WithMany("JornadasLaborales")
                        .HasForeignKey("TrabajadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trabajador");
                });

            modelBuilder.Entity("Stock.Models.Producto", b =>
                {
                    b.HasOne("Stock.Models.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaId");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Stock.Models.ProductoPorUsuario", b =>
                {
                    b.HasOne("Stock.Models.Producto", "Producto")
                        .WithMany("ProductosPorUsuario")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stock.Models.Usuario", "Usuario")
                        .WithMany("ProductosPorUsuario")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Stock.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Stock.Models.Producto", b =>
                {
                    b.Navigation("ProductosPorUsuario");
                });

            modelBuilder.Entity("Stock.Models.Trabajador", b =>
                {
                    b.Navigation("JornadasLaborales");
                });

            modelBuilder.Entity("Stock.Models.Usuario", b =>
                {
                    b.Navigation("ProductosPorUsuario");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using CardapioBolos.Banco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace cardapio_bolos.Migrations
{
    [DbContext(typeof(CardapioBolosContext))]
    [Migration("20241225214927_CriandoBancoInicial")]
    partial class CriandoBancoInicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BoloIngrediente", b =>
                {
                    b.Property<int>("BoloId")
                        .HasColumnType("int");

                    b.Property<int>("IngredienteId")
                        .HasColumnType("int");

                    b.HasKey("BoloId", "IngredienteId");

                    b.HasIndex("IngredienteId");

                    b.ToTable("BoloIngrediente");
                });

            modelBuilder.Entity("CardapioBolos.Model.Bolo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Bolos");
                });

            modelBuilder.Entity("CardapioBolos.Model.Ingrediente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredientes");
                });

            modelBuilder.Entity("BoloIngrediente", b =>
                {
                    b.HasOne("CardapioBolos.Model.Bolo", null)
                        .WithMany()
                        .HasForeignKey("BoloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardapioBolos.Model.Ingrediente", null)
                        .WithMany()
                        .HasForeignKey("IngredienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

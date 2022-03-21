﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace WEBPROJ.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20220320202145_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Ime")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prezime")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("Models.Prenociste", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Lokacija")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Naziv")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Prenocista");
                });

            modelBuilder.Entity("Models.Soba", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("PrenocisteID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PrenocisteID");

                    b.ToTable("Sobe");
                });

            modelBuilder.Entity("Models.Zakazivanje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int?>("SobaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("KorisnikID");

                    b.HasIndex("SobaID");

                    b.ToTable("Zakazivanja");
                });

            modelBuilder.Entity("Models.Soba", b =>
                {
                    b.HasOne("Models.Prenociste", "Prenociste")
                        .WithMany("Sobe")
                        .HasForeignKey("PrenocisteID");

                    b.Navigation("Prenociste");
                });

            modelBuilder.Entity("Models.Zakazivanje", b =>
                {
                    b.HasOne("Models.Korisnik", "Korisnik")
                        .WithMany("Zakazivanja")
                        .HasForeignKey("KorisnikID");

                    b.HasOne("Models.Soba", "Soba")
                        .WithMany("Zakazivanja")
                        .HasForeignKey("SobaID");

                    b.Navigation("Korisnik");

                    b.Navigation("Soba");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Navigation("Zakazivanja");
                });

            modelBuilder.Entity("Models.Prenociste", b =>
                {
                    b.Navigation("Sobe");
                });

            modelBuilder.Entity("Models.Soba", b =>
                {
                    b.Navigation("Zakazivanja");
                });
#pragma warning restore 612, 618
        }
    }
}

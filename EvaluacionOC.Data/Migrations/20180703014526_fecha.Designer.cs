﻿// <auto-generated />
using System;
using EvaluacionOC.Data.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EvaluacionOC.Data.Migrations
{
    [DbContext(typeof(EvalDbContext))]
    [Migration("20180703014526_fecha")]
    partial class fecha
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EvaluacionOC.Model.Model.Genero", b =>
                {
                    b.Property<byte>("Id");

                    b.Property<string>("Descripcion");

                    b.HasKey("Id");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("EvaluacionOC.Model.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<byte>("GeneroId");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("GeneroId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("EvaluacionOC.Model.Model.User", b =>
                {
                    b.HasOne("EvaluacionOC.Model.Model.Genero", "Genero")
                        .WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

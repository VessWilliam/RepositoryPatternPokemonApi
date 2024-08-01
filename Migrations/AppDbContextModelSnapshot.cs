﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SingleRepoPokemonApi.Data;

#nullable disable

namespace SingleRepoPokemonApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SingleRepoPokemonApi.Model.Entity.Pokemon", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("SingleRepoPokemonApi.Model.Entity.PokemonAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Def")
                        .HasColumnType("int");

                    b.Property<string>("PokemonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonAttribute");
                });

            modelBuilder.Entity("SingleRepoPokemonApi.Model.Entity.PokemonAttribute", b =>
                {
                    b.HasOne("SingleRepoPokemonApi.Model.Entity.Pokemon", "Pokemon")
                        .WithMany("PokemonAttributes")
                        .HasForeignKey("PokemonId");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("SingleRepoPokemonApi.Model.Entity.Pokemon", b =>
                {
                    b.Navigation("PokemonAttributes");
                });
#pragma warning restore 612, 618
        }
    }
}

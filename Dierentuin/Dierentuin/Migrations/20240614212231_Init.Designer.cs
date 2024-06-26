﻿// <auto-generated />
using System;
using Dierentuin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dierentuin.Migrations
{
    [DbContext(typeof(DierentuinContext))]
    [Migration("20240614212231_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Dierentuin.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityPattern")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("categories_id");

                    b.Property<int>("Dietary")
                        .HasColumnType("int");

                    b.Property<int?>("EnclosureId")
                        .HasColumnType("int")
                        .HasColumnName("enclosures_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("Prey")
                        .HasColumnType("int");

                    b.Property<int>("SecurityRequirement")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<double>("SpaceRequirement")
                        .HasColumnType("float")
                        .HasColumnName("spaceRequirement");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EnclosureId");

                    b.ToTable("animals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ActivityPattern = 0,
                            CategoryId = 4,
                            Dietary = 1,
                            EnclosureId = 9,
                            Name = "Fiona",
                            Prey = 4,
                            SecurityRequirement = 0,
                            Size = 4,
                            SpaceRequirement = 52.609020481924709,
                            Species = "Pachyderms"
                        },
                        new
                        {
                            Id = 2,
                            ActivityPattern = 2,
                            CategoryId = 9,
                            Dietary = 0,
                            EnclosureId = 1,
                            Name = "Nicklaus",
                            Prey = 7,
                            SecurityRequirement = 1,
                            Size = 0,
                            SpaceRequirement = 2.9302837644599737,
                            Species = "Pachyderms"
                        },
                        new
                        {
                            Id = 3,
                            ActivityPattern = 0,
                            CategoryId = 5,
                            Dietary = 0,
                            EnclosureId = 7,
                            Name = "Adolphus",
                            Prey = 9,
                            SecurityRequirement = 0,
                            Size = 3,
                            SpaceRequirement = 41.734799958790504,
                            Species = "Birds"
                        },
                        new
                        {
                            Id = 4,
                            ActivityPattern = 0,
                            CategoryId = 2,
                            Dietary = 4,
                            EnclosureId = 10,
                            Name = "Amaya",
                            Prey = 2,
                            SecurityRequirement = 1,
                            Size = 1,
                            SpaceRequirement = 6.6179048408281327,
                            Species = "Ungulates"
                        },
                        new
                        {
                            Id = 5,
                            ActivityPattern = 2,
                            CategoryId = 3,
                            Dietary = 4,
                            EnclosureId = 7,
                            Name = "Anabel",
                            Prey = 6,
                            SecurityRequirement = 0,
                            Size = 3,
                            SpaceRequirement = 4.9489435151673806,
                            Species = "Pachyderms"
                        },
                        new
                        {
                            Id = 6,
                            ActivityPattern = 1,
                            CategoryId = 5,
                            Dietary = 2,
                            EnclosureId = 5,
                            Name = "Enrique",
                            Prey = 4,
                            SecurityRequirement = 0,
                            Size = 2,
                            SpaceRequirement = 81.141339057972431,
                            Species = "Reptiles"
                        },
                        new
                        {
                            Id = 7,
                            ActivityPattern = 2,
                            CategoryId = 4,
                            Dietary = 1,
                            EnclosureId = 1,
                            Name = "Collin",
                            Prey = 7,
                            SecurityRequirement = 2,
                            Size = 2,
                            SpaceRequirement = 10.491983032470461,
                            Species = "Primates"
                        },
                        new
                        {
                            Id = 8,
                            ActivityPattern = 2,
                            CategoryId = 6,
                            Dietary = 0,
                            EnclosureId = 8,
                            Name = "Filiberto",
                            Prey = 9,
                            SecurityRequirement = 1,
                            Size = 4,
                            SpaceRequirement = 36.987949151280667,
                            Species = "Aquatic Animals"
                        },
                        new
                        {
                            Id = 9,
                            ActivityPattern = 1,
                            CategoryId = 9,
                            Dietary = 0,
                            EnclosureId = 8,
                            Name = "Vilma",
                            Prey = 3,
                            SecurityRequirement = 0,
                            Size = 4,
                            SpaceRequirement = 73.350852445513183,
                            Species = "Bats"
                        },
                        new
                        {
                            Id = 10,
                            ActivityPattern = 2,
                            CategoryId = 9,
                            Dietary = 3,
                            EnclosureId = 2,
                            Name = "Muriel",
                            Prey = 8,
                            SecurityRequirement = 2,
                            Size = 2,
                            SpaceRequirement = 22.775192582709639,
                            Species = "Crustaceans"
                        });
                });

            modelBuilder.Entity("Dierentuin.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Nulla vel ipsum.",
                            Name = "Birds"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Molestiae officia est.",
                            Name = "Mammals"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Est alias iure.",
                            Name = "Cetaceans"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Tempora cumque mollitia.",
                            Name = "Marsupials"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Odit excepturi sit.",
                            Name = "Mustelids"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Suscipit cumque adipisci.",
                            Name = "Rodents"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Numquam enim aliquid.",
                            Name = "Marsupials"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Incidunt quisquam nulla.",
                            Name = "Cetaceans"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Reiciendis debitis impedit.",
                            Name = "Bovines"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Et ut accusantium.",
                            Name = "Insects"
                        });
                });

            modelBuilder.Entity("Dierentuin.Models.Enclosure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Climate")
                        .HasColumnType("int");

                    b.Property<int>("Habitat")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SecurityLevel")
                        .HasColumnType("int");

                    b.Property<double>("Size")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("enclosures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Climate = 0,
                            Habitat = 0,
                            Name = "Reserve",
                            SecurityLevel = 1,
                            Size = 27.522662727696428
                        },
                        new
                        {
                            Id = 2,
                            Climate = 2,
                            Habitat = 3,
                            Name = "Dome",
                            SecurityLevel = 0,
                            Size = 83.072825068062414
                        },
                        new
                        {
                            Id = 3,
                            Climate = 0,
                            Habitat = 1,
                            Name = "House",
                            SecurityLevel = 0,
                            Size = 96.36872095566504
                        },
                        new
                        {
                            Id = 4,
                            Climate = 0,
                            Habitat = 0,
                            Name = "Habitat",
                            SecurityLevel = 1,
                            Size = 74.541575605704395
                        },
                        new
                        {
                            Id = 5,
                            Climate = 1,
                            Habitat = 1,
                            Name = "House",
                            SecurityLevel = 2,
                            Size = 44.015445261759993
                        },
                        new
                        {
                            Id = 6,
                            Climate = 1,
                            Habitat = 1,
                            Name = "Kooi",
                            SecurityLevel = 2,
                            Size = 35.207897451587122
                        },
                        new
                        {
                            Id = 7,
                            Climate = 2,
                            Habitat = 1,
                            Name = "Pavilion",
                            SecurityLevel = 1,
                            Size = 6.5657923927528081
                        },
                        new
                        {
                            Id = 8,
                            Climate = 0,
                            Habitat = 1,
                            Name = "Enclosure",
                            SecurityLevel = 0,
                            Size = 43.350064590647868
                        },
                        new
                        {
                            Id = 9,
                            Climate = 1,
                            Habitat = 1,
                            Name = "Outback",
                            SecurityLevel = 0,
                            Size = 11.322536233163754
                        },
                        new
                        {
                            Id = 10,
                            Climate = 2,
                            Habitat = 3,
                            Name = "Island",
                            SecurityLevel = 2,
                            Size = 21.247454788938654
                        });
                });

            modelBuilder.Entity("Dierentuin.Models.Animal", b =>
                {
                    b.HasOne("Dierentuin.Models.Category", "Category")
                        .WithMany("Animals")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Dierentuin.Models.Enclosure", "Enclosure")
                        .WithMany("Animal")
                        .HasForeignKey("EnclosureId");

                    b.Navigation("Category");

                    b.Navigation("Enclosure");
                });

            modelBuilder.Entity("Dierentuin.Models.Category", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("Dierentuin.Models.Enclosure", b =>
                {
                    b.Navigation("Animal");
                });
#pragma warning restore 612, 618
        }
    }
}
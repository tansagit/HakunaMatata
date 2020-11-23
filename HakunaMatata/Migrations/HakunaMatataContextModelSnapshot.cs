﻿// <auto-generated />
using System;
using HakunaMatata.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HakunaMatata.Migrations
{
    [DbContext(typeof(HakunaMatataContext))]
    partial class HakunaMatataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HakunaMatata.Models.DataModels.AboutUs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.ToTable("ABOUT_US");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Agent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActiveKey")
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<string>("AgentName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("ConfirmPhoneNumber")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("LoginName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(12)")
                        .HasMaxLength(12)
                        .IsUnicode(false);

                    b.Property<string>("ResetPasswordKey")
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<string>("Zalo")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("AGENT");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("CITY");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("DistrictName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("DISTRICT");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Faq", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .HasColumnType("ntext");

                    b.Property<string>("Question")
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.ToTable("FAQ");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("LEVEL");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<decimal?>("Longtitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<int?>("RealEstateId")
                        .HasColumnType("int");

                    b.Property<int?>("WardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("RealEstateId")
                        .IsUnique()
                        .HasName("UQ__MAP__C037863418DC284F")
                        .HasFilter("[RealEstateId] IS NOT NULL");

                    b.HasIndex("WardId");

                    b.ToTable("MAP");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PictureName")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int?>("RealEstateId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("URL")
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("RealEstateId");

                    b.ToTable("PICTURE");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PolicyContent")
                        .HasColumnType("ntext");

                    b.HasKey("Id");

                    b.ToTable("POLICY");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int>("RealEstateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RealEstateId");

                    b.ToTable("RATING");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.RealEstate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AgentId")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExprireTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("PostTime")
                        .HasColumnType("datetime");

                    b.Property<int?>("RealEstateTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("RealEstateTypeId");

                    b.ToTable("REAL_ESTATE");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.RealEstateDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Acreage")
                        .HasColumnType("int");

                    b.Property<bool>("AllowCook")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<int?>("ElectronicPrice")
                        .HasColumnType("int");

                    b.Property<bool>("FreeTime")
                        .HasColumnType("bit");

                    b.Property<bool>("HasMezzanine")
                        .HasColumnType("bit");

                    b.Property<bool>("HasPrivateWc")
                        .HasColumnName("HasPrivateWC")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<int?>("RealEstateId")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<bool>("SecurityCamera")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int?>("WaterPrice")
                        .HasColumnType("int");

                    b.Property<decimal?>("WifiPrice")
                        .HasColumnType("decimal(18, 0)");

                    b.HasKey("Id");

                    b.HasIndex("RealEstateId")
                        .IsUnique()
                        .HasName("UQ__REAL_EST__C0378634981FED9E")
                        .HasFilter("[RealEstateId] IS NOT NULL");

                    b.ToTable("REAL_ESTATE_DETAIL");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.RealEstateType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RealEstateTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("REAL_ESTATE_TYPE");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.SocialLogin", b =>
                {
                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Provider")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProviderKey")
                        .HasName("PK__SOCIAL_L__8DE43C5E9E312291");

                    b.HasIndex("UserId");

                    b.ToTable("SOCIAL_LOGIN");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Ward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("WardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("WARD");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Agent", b =>
                {
                    b.HasOne("HakunaMatata.Models.DataModels.Level", "Level")
                        .WithMany("Agent")
                        .HasForeignKey("LevelId")
                        .HasConstraintName("FK__AGENT__LevelId__3B75D760")
                        .IsRequired();
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.District", b =>
                {
                    b.HasOne("HakunaMatata.Models.DataModels.City", "City")
                        .WithMany("District")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK__DISTRICT__CityId__145C0A3F");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Map", b =>
                {
                    b.HasOne("HakunaMatata.Models.DataModels.City", "City")
                        .WithMany("Map")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK__MAP__CityId__48CFD27E");

                    b.HasOne("HakunaMatata.Models.DataModels.District", "District")
                        .WithMany("Map")
                        .HasForeignKey("DistrictId")
                        .HasConstraintName("FK__MAP__DistrictId__47DBAE45");

                    b.HasOne("HakunaMatata.Models.DataModels.RealEstate", "RealEstate")
                        .WithOne("Map")
                        .HasForeignKey("HakunaMatata.Models.DataModels.Map", "RealEstateId")
                        .HasConstraintName("FK__MAP__RealEstateI__49C3F6B7");

                    b.HasOne("HakunaMatata.Models.DataModels.Ward", "Ward")
                        .WithMany("Map")
                        .HasForeignKey("WardId")
                        .HasConstraintName("FK__MAP__WardId__46E78A0C");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Picture", b =>
                {
                    b.HasOne("HakunaMatata.Models.DataModels.RealEstate", "RealEstate")
                        .WithMany("Picture")
                        .HasForeignKey("RealEstateId")
                        .HasConstraintName("FK__PICTURE__RealEst__4CA06362");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Rating", b =>
                {
                    b.HasOne("HakunaMatata.Models.DataModels.RealEstate", "RealEstate")
                        .WithMany("Rating")
                        .HasForeignKey("RealEstateId")
                        .HasConstraintName("FK__RATING__RealEsta__4F7CD00D")
                        .IsRequired();
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.RealEstate", b =>
                {
                    b.HasOne("HakunaMatata.Models.DataModels.Agent", "Agent")
                        .WithMany("RealEstate")
                        .HasForeignKey("AgentId")
                        .HasConstraintName("FK__REAL_ESTA__Agent__3F466844");

                    b.HasOne("HakunaMatata.Models.DataModels.RealEstateType", "ReaEstateType")
                        .WithMany("RealEstate")
                        .HasForeignKey("RealEstateTypeId")
                        .HasConstraintName("FK__REAL_ESTA__ReaEs__3E52440B");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.RealEstateDetail", b =>
                {
                    b.HasOne("HakunaMatata.Models.DataModels.RealEstate", "RealEstate")
                        .WithOne("RealEstateDetail")
                        .HasForeignKey("HakunaMatata.Models.DataModels.RealEstateDetail", "RealEstateId")
                        .HasConstraintName("FK__REAL_ESTA__RealE__5629CD9C");
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.SocialLogin", b =>
                {
                    b.HasOne("HakunaMatata.Models.DataModels.Agent", "User")
                        .WithMany("SocialLogin")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__SOCIAL_LO__UserI__52593CB8")
                        .IsRequired();
                });

            modelBuilder.Entity("HakunaMatata.Models.DataModels.Ward", b =>
                {
                    b.HasOne("HakunaMatata.Models.DataModels.District", "District")
                        .WithMany("Ward")
                        .HasForeignKey("DistrictId")
                        .HasConstraintName("FK__WARD__DistrictId__173876EA");
                });
#pragma warning restore 612, 618
        }
    }
}

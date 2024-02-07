﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

#nullable disable

namespace NutritionAPI.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Energy", b =>
                {
                    b.Property<string>("FoodCode")
                        .HasColumnType("text");

                    b.Property<double?>("Kcal")
                        .HasColumnType("double precision");

                    b.Property<double?>("Kj")
                        .HasColumnType("double precision");

                    b.HasKey("FoodCode");

                    b.ToTable("Energy");
                });

            modelBuilder.Entity("Entities.Models.FoodGroups", b =>
                {
                    b.Property<string>("FoodGroupCode")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("FoodGroupCode");

                    b.ToTable("FoodGroups");
                });

            modelBuilder.Entity("Entities.Models.FoodItems", b =>
                {
                    b.Property<string>("FoodCode")
                        .HasColumnType("text");

                    b.Property<string>("DataReferences")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FoodGroupCode")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("FoodCode");

                    b.HasIndex("FoodGroupCode");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("Entities.Models.Macronutrients", b =>
                {
                    b.Property<string>("FoodCode")
                        .HasColumnType("text");

                    b.Property<double?>("Carbohydrate_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("Fat_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("Protein_g")
                        .HasColumnType("double precision");

                    b.HasKey("FoodCode");

                    b.ToTable("Macronutrients");
                });

            modelBuilder.Entity("Entities.Models.Minerals", b =>
                {
                    b.Property<string>("FoodCode")
                        .HasColumnType("text");

                    b.Property<double?>("Calcium_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Chloride_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Copper_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Iodine_mcg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Iron_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Magnesium_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Manganese_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Phosphorus_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Potassium_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Selenium_mcg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Sodium_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Zinc_mg")
                        .HasColumnType("double precision");

                    b.HasKey("FoodCode");

                    b.ToTable("Minerals");
                });

            modelBuilder.Entity("Entities.Models.Proximates", b =>
                {
                    b.Property<string>("FoodCode")
                        .HasColumnType("text");

                    b.Property<double?>("Alcohol_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("Cholesterol_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("FatsMonounsaturated_g")
                        .HasColumnType("double precision")
                        .HasColumnName("Fats_Monounsaturated_g");

                    b.Property<double?>("FatsPolyunsaturated_g")
                        .HasColumnType("double precision")
                        .HasColumnName("Fats_Polyunsaturated_g");

                    b.Property<double?>("FatsSaturated_g")
                        .HasColumnType("double precision")
                        .HasColumnName("Fats_Saturated_g");

                    b.Property<double?>("FatsTrans_g")
                        .HasColumnType("double precision")
                        .HasColumnName("Fats_Trans_g");

                    b.Property<double?>("Fibre_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("Fructose_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("Galactose_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("Glucose_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("Lactose_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("Maltose_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("NonStarchPolysaccharides_g")
                        .HasColumnType("double precision")
                        .HasColumnName("Non_Starch_Polysaccharides_g");

                    b.Property<double?>("Starch_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("Sucrose_g")
                        .HasColumnType("double precision");

                    b.Property<double?>("TotalSugars_g")
                        .HasColumnType("double precision")
                        .HasColumnName("Total_Sugars_g");

                    b.Property<double?>("Water_g")
                        .HasColumnType("double precision");

                    b.HasKey("FoodCode");

                    b.ToTable("Proximates");
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Vitamins", b =>
                {
                    b.Property<string>("FoodCode")
                        .HasColumnType("text");

                    b.Property<double?>("Biotin_mcg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Carotene_mcg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Folate_mcg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Niacin_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Pantothenate_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Retinol_mcg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Riboflavin_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Thiamin_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("Trytophan_mg")
                        .HasColumnType("double precision");

                    b.Property<double?>("VitaminB12_mcg")
                        .HasColumnType("double precision")
                        .HasColumnName("Vitamin_B12_mcg");

                    b.Property<double?>("VitaminB6_mg")
                        .HasColumnType("double precision")
                        .HasColumnName("Vitamin_B6_mg");

                    b.Property<double?>("VitaminC_mg")
                        .HasColumnType("double precision")
                        .HasColumnName("Vitamin_C_mg");

                    b.Property<double?>("VitaminD_mcg")
                        .HasColumnType("double precision")
                        .HasColumnName("Vitamin_D_mcg");

                    b.Property<double?>("VitaminE_mg")
                        .HasColumnType("double precision")
                        .HasColumnName("Vitamin_E_mg");

                    b.Property<double?>("VitaminK1_mcg")
                        .HasColumnType("double precision")
                        .HasColumnName("Vitamin_K1_mcg");

                    b.HasKey("FoodCode");

                    b.ToTable("Vitamins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "7914e0ec-0206-4c1c-9224-38e79e390cbb",
                            ConcurrencyStamp = "6b9a1cf1-fff7-4233-8e2f-14c9713db5a1",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Energy", b =>
                {
                    b.HasOne("Entities.Models.FoodItems", "FoodItems")
                        .WithOne("Energy")
                        .HasForeignKey("Entities.Models.Energy", "FoodCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("Entities.Models.FoodItems", b =>
                {
                    b.HasOne("Entities.Models.FoodGroups", "FoodGroup")
                        .WithMany("FoodItemsCollection")
                        .HasForeignKey("FoodGroupCode");

                    b.Navigation("FoodGroup");
                });

            modelBuilder.Entity("Entities.Models.Macronutrients", b =>
                {
                    b.HasOne("Entities.Models.FoodItems", "FoodItems")
                        .WithOne("Macronutrients")
                        .HasForeignKey("Entities.Models.Macronutrients", "FoodCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("Entities.Models.Minerals", b =>
                {
                    b.HasOne("Entities.Models.FoodItems", "FoodItems")
                        .WithOne("Minerals")
                        .HasForeignKey("Entities.Models.Minerals", "FoodCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("Entities.Models.Proximates", b =>
                {
                    b.HasOne("Entities.Models.FoodItems", "FoodItems")
                        .WithOne("Proximates")
                        .HasForeignKey("Entities.Models.Proximates", "FoodCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("Entities.Models.Vitamins", b =>
                {
                    b.HasOne("Entities.Models.FoodItems", "FoodItems")
                        .WithOne("Vitamins")
                        .HasForeignKey("Entities.Models.Vitamins", "FoodCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItems");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.FoodGroups", b =>
                {
                    b.Navigation("FoodItemsCollection");
                });

            modelBuilder.Entity("Entities.Models.FoodItems", b =>
                {
                    b.Navigation("Energy");

                    b.Navigation("Macronutrients");

                    b.Navigation("Minerals");

                    b.Navigation("Proximates");

                    b.Navigation("Vitamins");
                });
#pragma warning restore 612, 618
        }
    }
}

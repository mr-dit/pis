﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using pis;

#nullable disable

namespace pis_web_api.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("pis_web_api.Models.db.Animal", b =>
                {
                    b.Property<int>("RegistrationNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RegistrationNumber"));

                    b.Property<int>("AnimalCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("AnimalName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ElectronicChipNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GenderId")
                        .HasColumnType("integer");

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("text");

                    b.Property<string>("SpecialSigns")
                        .HasColumnType("text");

                    b.Property<int>("YearOfBirth")
                        .HasColumnType("integer");

                    b.HasKey("RegistrationNumber");

                    b.HasIndex("AnimalCategoryId");

                    b.HasIndex("GenderId");

                    b.HasIndex("LocalityId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("pis_web_api.Models.db.AnimalCategory", b =>
                {
                    b.Property<int>("IdAnimalCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdAnimalCategory"));

                    b.Property<string>("NameAnimalCategory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdAnimalCategory");

                    b.HasIndex("NameAnimalCategory")
                        .IsUnique();

                    b.ToTable("AnimalCategories");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Contract", b =>
                {
                    b.Property<int>("IdContract")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdContract"));

                    b.Property<DateOnly>("ConclusionDate")
                        .HasColumnType("date");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("ExpirationDate")
                        .HasColumnType("date");

                    b.Property<int>("PerformerId")
                        .HasColumnType("integer");

                    b.HasKey("IdContract");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PerformerId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Gender", b =>
                {
                    b.Property<int>("IdGender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdGender"));

                    b.Property<string>("NameGender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdGender");

                    b.HasIndex("NameGender")
                        .IsUnique();

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Journal", b =>
                {
                    b.Property<int>("JounalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("JounalID"));

                    b.Property<int>("ActionType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DescriptionObject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EditID")
                        .HasColumnType("integer");

                    b.Property<int>("TableName")
                        .HasColumnType("integer");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("JounalID");

                    b.HasIndex("UserID");

                    b.ToTable("Journals");
                });

            modelBuilder.Entity("pis_web_api.Models.db.LocalitisListForContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ContractId")
                        .HasColumnType("integer");

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("LocalityId");

                    b.ToTable("LocalitisListForContract");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Locality", b =>
                {
                    b.Property<int>("IdLocality")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdLocality"));

                    b.Property<string>("NameLocality")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdLocality");

                    b.HasIndex("NameLocality")
                        .IsUnique();

                    b.ToTable("Localitis");
                });

            modelBuilder.Entity("pis_web_api.Models.db.OrgType", b =>
                {
                    b.Property<int>("IdOrgType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdOrgType"));

                    b.Property<string>("NameOrgType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdOrgType");

                    b.HasIndex("NameOrgType")
                        .IsUnique();

                    b.ToTable("OrgTypes");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Organisation", b =>
                {
                    b.Property<int>("OrgId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrgId"));

                    b.Property<string>("AdressReg")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("INN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("KPP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer");

                    b.Property<string>("OrgName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrgTypeId")
                        .HasColumnType("integer");

                    b.HasKey("OrgId");

                    b.HasIndex("LocalityId");

                    b.HasIndex("OrgTypeId");

                    b.ToTable("Organisations");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Role", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdRole"));

                    b.Property<string>("NameRole")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdRole");

                    b.HasIndex("NameRole")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("pis_web_api.Models.db.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdUser"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrganisationId")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdUser");

                    b.HasIndex("OrganisationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("pis_web_api.Models.db.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserRoleId"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Vaccination", b =>
                {
                    b.Property<int>("IdVactination")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdVactination"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("integer");

                    b.Property<int>("ContractId")
                        .HasColumnType("integer");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("VaccinationDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("VaccinationValidDate")
                        .HasColumnType("date");

                    b.Property<int>("VaccineId")
                        .HasColumnType("integer");

                    b.Property<string>("VaccineSeriesNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdVactination");

                    b.HasIndex("AnimalId");

                    b.HasIndex("ContractId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("VaccineId");

                    b.ToTable("Vaccinations");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Vaccine", b =>
                {
                    b.Property<int>("IdVaccine")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdVaccine"));

                    b.Property<string>("NameVaccine")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ValidDaysVaccine")
                        .HasColumnType("integer");

                    b.HasKey("IdVaccine");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Animal", b =>
                {
                    b.HasOne("pis_web_api.Models.db.AnimalCategory", "AnimalCategory")
                        .WithMany()
                        .HasForeignKey("AnimalCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pis_web_api.Models.db.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pis_web_api.Models.db.Locality", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalCategory");

                    b.Navigation("Gender");

                    b.Navigation("Locality");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Contract", b =>
                {
                    b.HasOne("pis_web_api.Models.db.Organisation", "Customer")
                        .WithMany("ContractsAsCustomer")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pis_web_api.Models.db.Organisation", "Performer")
                        .WithMany("ContractsAsPerformer")
                        .HasForeignKey("PerformerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Performer");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Journal", b =>
                {
                    b.HasOne("pis_web_api.Models.db.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("pis_web_api.Models.db.LocalitisListForContract", b =>
                {
                    b.HasOne("pis_web_api.Models.db.Contract", "Contract")
                        .WithMany("Localities")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pis_web_api.Models.db.Locality", "Locality")
                        .WithMany("Contracts")
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Locality");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Organisation", b =>
                {
                    b.HasOne("pis_web_api.Models.db.Locality", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pis_web_api.Models.db.OrgType", "OrgType")
                        .WithMany()
                        .HasForeignKey("OrgTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Locality");

                    b.Navigation("OrgType");
                });

            modelBuilder.Entity("pis_web_api.Models.db.User", b =>
                {
                    b.HasOne("pis_web_api.Models.db.Organisation", "Organisation")
                        .WithMany("Users")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisation");
                });

            modelBuilder.Entity("pis_web_api.Models.db.UserRole", b =>
                {
                    b.HasOne("pis_web_api.Models.db.Role", "Role")
                        .WithMany("UsersRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pis_web_api.Models.db.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Vaccination", b =>
                {
                    b.HasOne("pis_web_api.Models.db.Animal", "Animal")
                        .WithMany("Vaccinations")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pis_web_api.Models.db.Contract", "Contract")
                        .WithMany("Vaccinations")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pis_web_api.Models.db.User", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pis_web_api.Models.db.Vaccine", "Vaccine")
                        .WithMany()
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Contract");

                    b.Navigation("Doctor");

                    b.Navigation("Vaccine");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Animal", b =>
                {
                    b.Navigation("Vaccinations");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Contract", b =>
                {
                    b.Navigation("Localities");

                    b.Navigation("Vaccinations");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Locality", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Organisation", b =>
                {
                    b.Navigation("ContractsAsCustomer");

                    b.Navigation("ContractsAsPerformer");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("pis_web_api.Models.db.Role", b =>
                {
                    b.Navigation("UsersRole");
                });

            modelBuilder.Entity("pis_web_api.Models.db.User", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}

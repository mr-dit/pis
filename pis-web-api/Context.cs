using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using pis_web_api.Models.db;
using pis_web_api.Models.db.ReportStates;

namespace pis
{
    public class Context : DbContext
    {
        private static bool isCreate = false;

        public Context()
        {
            if (!isCreate)
            {
                //Database.EnsureDeleted();
                //Database.EnsureCreated();
                isCreate = true;
            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string stringConnection = Environment.GetEnvironmentVariable("dbStringConectionPostgres", EnvironmentVariableTarget.Machine);
            optionsBuilder.UseNpgsql("host=mks-server.tplinkdns.com;port=5432;Database=vaccinations;Username=mksti;Password=mks;Include Error Detail=true");
            //host=mks-server.tplinkdns.com;port=5432;Database=vaccinations;Username=mksti;Password=mks;Include Error Detail=true
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.ContractsAsCustomer)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<Contract>()
                .HasOne(x => x.Performer)
                .WithMany(x => x.ContractsAsPerformer)
                .HasForeignKey(x => x.PerformerId);

            modelBuilder.Entity<Role>()
                .HasIndex(x => x.NameRole)
                .IsUnique();

            modelBuilder.Entity<Gender>()
                .HasIndex(x => x.NameGender)
                .IsUnique();

            modelBuilder.Entity<Locality>()
                .HasIndex(x => x.NameLocality)
                .IsUnique();

            modelBuilder.Entity<AnimalCategory>()
                .HasIndex(x => x.NameAnimalCategory)
                .IsUnique();

            modelBuilder.Entity<OrgType>()
                .HasIndex(x => x.NameOrgType)
                .IsUnique();

            modelBuilder.Entity<Report>()
                .HasMany(x => x.StatisticaHolders)
                .WithOne(x => x.Report)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StatisticaHolder>()
                .HasMany(x => x.VaccinePrice)
                .WithOne(x => x.StatisticaHolder)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Locality> Localitis { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<OrgType> OrgTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AnimalCategory> AnimalCategories { get; set; }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<LocalitisListForContract> LocalitisListForContract { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRole {  get; set; }

        public DbSet<Journal> Journals { get; set; }

        public DbSet<Report> Reports { get; set; }
        public DbSet<StatisticaHolder> StatisticaHolder { get; set; }
        public DbSet<StatisticItem> StatisticItems { get; set; }

    }
}

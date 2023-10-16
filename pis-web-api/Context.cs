using Microsoft.EntityFrameworkCore;
using pis.Models;

namespace pis
{
    public class Context : DbContext
    {
        private static bool isCreate = false;

        public Context()
        {
            if (!isCreate)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
                isCreate = true;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=mks-server.tplinkdns.com;port=5432;Database=vaccinations;Username=mksti;Password=mks;Include Error Detail=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<Contract>()
                .HasMany(x => x.Localities)
                .WithMany(x => x.Contracts)
                .UsingEntity(x => x.ToTable("LocalitiesListForContract"));

            modelBuilder.Entity<Post>()
                .HasIndex(x => x.NamePost)
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
        }

        public DbSet<Locality> Localitis { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<OrgType> OrgTypes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<AnimalCategory> AnimalCategories { get; set; }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<VaccinePriceListByLocality> PriceList { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

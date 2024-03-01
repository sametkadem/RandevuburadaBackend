using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using randevuburada.EntityLayer.Concrete.Location;
using randevuburada.EntityLayer.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;

namespace randevuburada.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser,AppRole,int>

    {
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=samet\\SQLEXPRESS;initial catalog=randevuburadaDb;integrated security=true;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
                b.ToTable("UserLogins");
            });

            modelBuilder.Entity<Customer>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Customer>()
                .Property(c => c.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnUpdate();
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Customer> Customers { get; set; }


        public DbSet<CompanyBankingDetails> CompanyBankingDetails { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyPackage> CompanyPackages { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }



    }
}

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
using randevuburada.EntityLayer.Concrete.Other;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Staff;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using randevuburada.EntityLayer.Concrete.ChatConcrete;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace randevuburada.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser,AppRole,int>

    {
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=tcp:randevuburada.database.windows.net,1433;Initial Catalog=Randevuburada.WebApi_db;Persist Security Info=False;User ID=master;Password=smtKDM110*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            optionsBuilder.UseSqlServer("Server=77.245.159.27\\MSSQLSERVER2019;database=randevuburadaDb;user=master;password=?7n7hLf54;TrustServerCertificate=true");
            //optionsBuilder.UseSqlServer("Data Source=tcp:randevuburada.database.windows.net,1433;Initial Catalog=Randevuburada.WebApi_db;User Id=master@randevuburada;Password=smtKDM110*");
            //optionsBuilder.UseSqlServer("Server=samet\\SQLEXPRESS;initial catalog=randevuburadaDb;integrated security=true;TrustServerCertificate=True;");
            //            optionsBuilder.UseSqlServer("Server=tcp:randevuburada.database.windows.net,1433;Initial Catalog=Randevuburada.WebApi_db;Persist Security Info=False;User ID=master;Password=smtKDM110*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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

        public DbSet<CompanyOwnerInfo> CompanyOwnerInfos { get; set; }
        public DbSet<CompanySubscribe> CompanySubscribes { get; set; }

        public DbSet<Day> Days { get; set; }
        public DbSet<Gender> Genders { get; set; }
        
        public DbSet<CompanyWorkingHours> CompanyWorkingHours { get; set;}
        public DbSet<CompanyStaff> CompanyStaffs { get; set;}
        public DbSet<CompanyService> CompanyServices { get; set; }

        public DbSet<StaffWorkingStatus> StaffWorkingStatus { get; set; }
        public DbSet<StaffWorkingHours> StaffWorkingHours { get; set; }
        public DbSet<StaffWorkingPosition> StaffWorkingPosition { get; set; }

        public DbSet<ServiceStaff> ServiceStaff { get; set; }
        public DbSet<ServiceIntervalHours> ServiceIntervalHours { get; set; }
        public DbSet<StaffWorkingPosition> StaffWorkingPositions { get; set; }

        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<CompanyMedia> CompanyMedia { get; set; }

        public DbSet<CompanySocialMedia> CompanySocialMedia { get; set;}

        public DbSet<AppointmentCompanyInfo> AppointmentCompanyInfo { get; set;}
        public DbSet<AppointmentInfo> AppointmentInfo { get; set; }
        public DbSet<GeneralAppointment> GeneralAppointment { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatus { get; set; }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatStatus> ChatStatus { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<PaymentType> PaymentType { get; set; }

        public DbSet<CustomerComment> CustomerComment { get; set; }
        public DbSet<CustomerFavourite> CustomerFavourite { get; set;}

        public DbSet<CustomerAppointmentInfo> CustomerAppointmentInfo { get; set; }
        public DbSet<CustomerBillingInfo> CustomerBillingInfo { get; set; }

        public DbSet<MainService> MainService { get; set; }

    }
}

using randevuburada.EntityLayer.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CompanyConcrete
{
    public class Company
    {
        public int Id { get; set; }
        public int CompanyTypeId { get; set; }
        public CompanyType CompanyType { get; set; }
        public int CompanyPackagesId { get; set; }
        public CompanyPackage CompanyPackage { get; set; }
        public int CompanyBankingDetailsId { get; set; }
        public CompanyBankingDetails CompanyBankingDetails { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
        public required AppUser AppUser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}

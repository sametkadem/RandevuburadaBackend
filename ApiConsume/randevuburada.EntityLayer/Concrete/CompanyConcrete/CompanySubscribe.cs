using randevuburada.EntityLayer.Concrete.Identity;
using randevuburada.EntityLayer.Concrete.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CompanyConcrete
{
    public class CompanySubscribe
    {
        public int Id { get; private set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int companyPackageId {  get; set; }
        public CompanyPackage Package { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}

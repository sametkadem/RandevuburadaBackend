using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CompanyConcrete
{
    public class CompanyPackage
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public int MaxSms { get; set; }
        public int PackageDay { get; set; }
        public int MaxBranch { get; set; }
        public int PackagePrice { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}

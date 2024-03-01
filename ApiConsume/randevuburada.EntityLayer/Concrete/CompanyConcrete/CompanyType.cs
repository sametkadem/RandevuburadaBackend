using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CompanyConcrete
{
    public class CompanyType
    {
        public int Id { get; set; }
        public string CompanyTypeName { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}

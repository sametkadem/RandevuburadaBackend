using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CompanyConcrete.Service
{
    public class ServiceStaff
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int ServiceId { get; set; }
        public CompanyService CompanyService { get; set; }
    }
}

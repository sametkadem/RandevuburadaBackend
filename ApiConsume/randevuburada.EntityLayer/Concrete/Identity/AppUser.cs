using Microsoft.AspNetCore.Identity;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.Identity
{
    public class AppUser:IdentityUser<int>
    {
        public int UserTypeId { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Company> Companies { get; set; }
        public ICollection<Customer> Customers { get; set; }

    }
}

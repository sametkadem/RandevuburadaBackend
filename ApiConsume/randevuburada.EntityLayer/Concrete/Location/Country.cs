using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.Location
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<District> Districts { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}

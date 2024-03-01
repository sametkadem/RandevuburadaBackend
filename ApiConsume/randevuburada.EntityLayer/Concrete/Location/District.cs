using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.Location
{
    public class District
    {
        public int Id { get; set; }
        public string DistrictName { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Customer> Customers { get; set; }

    }
}

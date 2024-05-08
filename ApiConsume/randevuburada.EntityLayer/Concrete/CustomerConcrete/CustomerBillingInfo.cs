using randevuburada.EntityLayer.Concrete.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CustomerConcrete
{
    public class CustomerBillingInfo
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public string? BillingZipCode { get; set; }
        public string? BillingPhone { get; set; }
        public string? BillingTcNo { get; set; }
        public string? BillingCompanyName { get; set; }
        public string? BillingTaxNo { get; set; }
        public string? BillingTaxOffice { get; set; }
        public bool Commercial { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

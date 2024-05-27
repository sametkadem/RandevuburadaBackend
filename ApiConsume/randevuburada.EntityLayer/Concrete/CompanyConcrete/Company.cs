using randevuburada.EntityLayer.Concrete.Identity;
using randevuburada.EntityLayer.Concrete.Location;
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
        public bool CompanyStatus { get; set; }
        public bool CompanyVisibility { get; set; }
        public int CompanyTypeId { get; set; }
        public CompanyType CompanyType { get; set; }
        public int CompanyBankingDetailsId { get; set; }
        public CompanyBankingDetails CompanyBankingDetails { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? CompanyAdress { get; set; }
        public string? CompanyAbout {  get; set; }
        public bool IsDisabledAccessiblity { get; set; }
        public int UserId { get; set; }
        public required AppUser User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}

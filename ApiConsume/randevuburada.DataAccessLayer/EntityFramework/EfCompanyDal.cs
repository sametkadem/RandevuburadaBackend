using Microsoft.EntityFrameworkCore;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfCompanyDal : GenericRepository<Company>, ICompanyDal
    {
        public EfCompanyDal(Context context) : base(context)
        {
        }

        public IEnumerable<Company> GetByUserID(int userId)
        {
            var context = new Context();
            return context.Companies.Where(x => x.UserId == userId).ToList();
        }

        public int GetCountCompanyByUserId(int userId)
        {
            var context = new Context();
            return context.Companies.Count(x => x.UserId == userId);
        }

        public IEnumerable<Company> GetCountryCityDistrictCompany(int countryId, int cityId, int districtId)
        {
            var context = new Context();

            if (districtId == 0)
            {
                return context.Companies
                    .Where(x => x.CountryId == countryId && x.CityId == cityId)
                    .ToList();
            }
            else if (cityId == 0)
            {
                return context.Companies
                    .Where(x => x.CountryId == countryId)
                    .ToList();
            }
            return context.Companies
                .Where(x => x.CountryId == countryId && x.CityId == cityId && x.DistrictId == districtId)
                .ToList();
        }

        public Company updateCompany(Company company)
        {
            var context = new Context();
            var findCompany = context.Companies.Find(company.Id);
            findCompany.CompanyName = company.CompanyName;
            findCompany.PhoneNumber = company.PhoneNumber;
            findCompany.Email = company.Email;
            findCompany.CompanyAdress = company.CompanyAdress;
            findCompany.CountryId = company.CountryId;
            findCompany.CityId = company.CityId;
            findCompany.DistrictId = company.DistrictId;
            findCompany.CompanyLogo = company.CompanyLogo;
            findCompany.CompanyVisibility = company.CompanyVisibility;
            findCompany.CompanyStatus = company.CompanyStatus;
            findCompany.CompanyAbout = company.CompanyAbout;
            findCompany.CompanyBankingDetailsId = company.CompanyBankingDetailsId;
            findCompany.Website = company.Website;
            findCompany.Latitude = company.Latitude;
            findCompany.Longitude = company.Longitude;
            findCompany.UserId = company.UserId;
            findCompany.UpdatedAt = DateTime.Now;
            findCompany.CompanyTypeId = company.CompanyTypeId;
            
            var updatedEntity = context.Entry(findCompany);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
            return findCompany;
        }
    }
}

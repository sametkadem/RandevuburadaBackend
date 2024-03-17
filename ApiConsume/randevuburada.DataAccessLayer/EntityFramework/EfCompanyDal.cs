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
    }
}

using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface ICompanyService : IGenericService<Company>
    {
        public int TGetCountCompanyByUserId(int userId);
        public IEnumerable<Company> TGetByUserID(int userId);

        public IEnumerable<Company> TGetCountryCityDistrictCompany(int countryId, int cityId, int districtId);

        public Company TupdateCompany(Company company);
    }
}

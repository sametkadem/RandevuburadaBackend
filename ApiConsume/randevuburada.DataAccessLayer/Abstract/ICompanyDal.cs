using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICompanyDal : IGenericDal<Company>
    {
        public int GetCountCompanyByUserId(int userId);
        public IEnumerable<Company> GetByUserID(int userId);
        public IEnumerable<Company> GetCountryCityDistrictCompany(int countryId, int cityId, int districtId);
    }
}

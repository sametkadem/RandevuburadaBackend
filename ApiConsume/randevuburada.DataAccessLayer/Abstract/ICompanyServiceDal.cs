using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICompanyServiceDal : IGenericDal<CompanyService>
    {
        public List<CompanyService> GetByCompanyId(int companyId);
        public Task<List<CompanyService>> GetByCompanyIdAsync(int companyId);

    }


}

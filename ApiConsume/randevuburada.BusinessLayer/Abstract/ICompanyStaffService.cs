using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface ICompanyStaffService:IGenericService<CompanyStaff>
    {
        public List<CompanyStaff> TGetByCompanyId (int companyId);
    }
}

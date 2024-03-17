using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface ICompanyWorkingHoursService:IGenericService<CompanyWorkingHours>
    {
        public List<int> THasCompanyWorkingHours(int companyId, int[] dayIds);
    }


}

using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyWorkingHoursDto;
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
        public List<CompanyWorkingHours> TGetByCompanyId(int companyId);
        public void TDeleteByCompanyId(int companyId);
        public void TUpdateByCompanyId(int companyId, CompanyWorkingHoursUpdateDto companyWorkingHours);
    }
}

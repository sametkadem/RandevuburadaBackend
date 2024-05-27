using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyWorkingHoursDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICompanyWorkingHoursDal : IGenericDal<CompanyWorkingHours>
    {
        public List<int> HasCompanyWorkingHours(int companyId, int[] dayIds);
        public List<CompanyWorkingHours> GetByCompanyId(int companyId);
        public void DeleteByCompanyId(int companyId);
        public void UpdateByCompanyId(int companyId, CompanyWorkingHoursUpdateDto companyWorkingHours);
        public CompanyWorkingHours GetByCompanyIdAndDayId(int companyId, int dayId);
        public int updateOrInsertCompanyWorkingHours(CompanyWorkingHours companyWorkingHours);

    }
}

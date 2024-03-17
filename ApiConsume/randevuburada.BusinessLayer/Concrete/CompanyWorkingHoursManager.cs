using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class CompanyWorkingHoursManager : ICompanyWorkingHoursService
    {
        private readonly ICompanyWorkingHoursDal _companyWorkingHoursDal;

        public CompanyWorkingHoursManager(ICompanyWorkingHoursDal companyWorkingHoursDal)
        {
            _companyWorkingHoursDal = companyWorkingHoursDal;
        }

        public void TDelete(CompanyWorkingHours t)
        {
            _companyWorkingHoursDal.Delete(t);
        }

        public CompanyWorkingHours TGetByID(int id)
        {
            return _companyWorkingHoursDal.GetByID(id);
        }

        public List<CompanyWorkingHours> TGetList()
        {
            return _companyWorkingHoursDal.GetList();
        }

        public List<int> THasCompanyWorkingHours(int companyId, int[] dayIds)
        {
            return _companyWorkingHoursDal.HasCompanyWorkingHours(companyId, dayIds);
        }

        public void TInsert(CompanyWorkingHours t)
        {
            _companyWorkingHoursDal.Insert(t);
        }

        public void TUpdate(CompanyWorkingHours t)
        {
            _companyWorkingHoursDal.Update(t);
        }
    }
}

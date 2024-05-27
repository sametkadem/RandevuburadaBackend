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
    public class CompanyServiceManager : ICompanyServiceService
    {
        private readonly ICompanyServiceDal _companyServiceDal;

        public CompanyServiceManager(ICompanyServiceDal companyServiceDal)
        {
            _companyServiceDal = companyServiceDal;
        }

        public void TDelete(CompanyService t)
        {
            _companyServiceDal.Delete(t);
        }

        public List<CompanyService> TGetByCompanyId(int companyId)
        {
            return _companyServiceDal.GetByCompanyId(companyId);
        }

        public CompanyService TGetByCompanyIdAndServiceId(int companyId, int serviceId)
        {
            return _companyServiceDal.GetByCompanyIdAndServiceId(companyId, serviceId);
        }

        public Task<List<CompanyService>> TGetByCompanyIdAsync(int companyId)
        {
            return _companyServiceDal.GetByCompanyIdAsync(companyId);
        }

        public CompanyService TGetByID(int id)
        {
            return _companyServiceDal.GetByID(id);
        }

        public List<CompanyService> TGetList()
        {
            return _companyServiceDal.GetList();
        }

        public void TInsert(CompanyService t)
        {
            _companyServiceDal.Insert(t);
        }

        public void TUpdate(CompanyService t)
        {
            _companyServiceDal.Update(t);
        }

        public CompanyService TupdateCompanyService(CompanyService companyService)
        {
            return _companyServiceDal.updateCompanyService(companyService);
        }
    }
}

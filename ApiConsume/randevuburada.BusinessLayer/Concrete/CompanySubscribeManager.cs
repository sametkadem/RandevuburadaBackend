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
    public class CompanySubscribeManager : ICompanySubscribeService
    {
        private readonly ICompanySubscribeDal _companySubscribeDal;

        public CompanySubscribeManager(ICompanySubscribeDal companySubscribeDal)
        {
            _companySubscribeDal = companySubscribeDal;
        }

        public void TDelete(CompanySubscribe t)
        {
            _companySubscribeDal.Delete(t);
        }

        public CompanySubscribe TGetByID(int id)
        {
            return _companySubscribeDal.GetByID(id);
        }

        public CompanySubscribe TGetByUserID(int id)
        {
            return _companySubscribeDal.GetByUserID(id);
        }

        public List<CompanySubscribe> TGetList()
        {
            return _companySubscribeDal.GetList();
        }

        public void TInsert(CompanySubscribe t)
        {
            _companySubscribeDal.Insert(t);
        }

        public void TUpdate(CompanySubscribe t)
        {
            _companySubscribeDal.Update(t);
        }

        public bool TCheckCompany(int userId)
        {
            return _companySubscribeDal.CheckCompany(userId);
        }
    }
}

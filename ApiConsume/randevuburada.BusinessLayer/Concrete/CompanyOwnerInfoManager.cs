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
    public class CompanyOwnerInfoManager : ICompanyOwnerInfoService
    {
        public readonly ICompanyOwnerInfoDal _companyOwnerInfoDal;

        public CompanyOwnerInfoManager(ICompanyOwnerInfoDal companyOwnerInfoDal)
        {
            _companyOwnerInfoDal = companyOwnerInfoDal;
        }
        public void TDelete(CompanyOwnerInfo t)
        {
            _companyOwnerInfoDal.Delete(t);
        }

        public CompanyOwnerInfo TGetByID(int id)
        {
            return _companyOwnerInfoDal.GetByID(id);
        }

        public List<CompanyOwnerInfo> TGetList()
        {
            return _companyOwnerInfoDal.GetList();
        }

        public void TInsert(CompanyOwnerInfo t)
        {
            _companyOwnerInfoDal.Insert(t);
        }

        public void TUpdate(CompanyOwnerInfo t)
        {
            _companyOwnerInfoDal.Update(t);
        }

        public bool TCheckCompany(int userId)
        {
            return _companyOwnerInfoDal.CheckCompany(userId);
        }

        public CompanyOwnerInfo TGetByUserID(int userId)
        {
            return _companyOwnerInfoDal.GetByUserID(userId);
        }
    }
}

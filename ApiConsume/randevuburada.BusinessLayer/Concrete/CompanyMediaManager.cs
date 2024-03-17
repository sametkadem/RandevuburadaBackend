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
    public class CompanyMediaManager : ICompanyMediaService
    {
        private readonly ICompanyMediaDal _companyMediaDal;

        public CompanyMediaManager(ICompanyMediaDal companyMediaDal)
        {
            _companyMediaDal = companyMediaDal;
        }

        public void TDelete(CompanyMedia t)
        {
            _companyMediaDal.Delete(t);
        }

        public CompanyMedia TGetByID(int id)
        {
            return _companyMediaDal.GetByID(id);
        }

        public List<CompanyMedia> TGetList()
        {
            return _companyMediaDal.GetList();
        }

        public void TInsert(CompanyMedia t)
        {
            _companyMediaDal.Insert(t);
        }

        public void TUpdate(CompanyMedia t)
        {
            _companyMediaDal.Update(t);
        }
    }
}

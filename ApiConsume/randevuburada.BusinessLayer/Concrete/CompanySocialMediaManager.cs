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
    public class CompanySocialMediaManager : ICompanySocialMediaService
    {
        private readonly ICompanySocialMediaDal _companySocialMediaDal;

        public CompanySocialMediaManager(ICompanySocialMediaDal companySocialMediaDal)
        {
            _companySocialMediaDal = companySocialMediaDal;
        }

        public void TDelete(CompanySocialMedia t)
        {
            _companySocialMediaDal.Delete(t);
        }

        public CompanySocialMedia TGetByID(int id)
        {
            return _companySocialMediaDal.GetByID(id);
        }

        public List<CompanySocialMedia> TGetList()
        {
            return _companySocialMediaDal.GetList();
        }

        public void TInsert(CompanySocialMedia t)
        {
            _companySocialMediaDal.Insert(t);
        }

        public void TUpdate(CompanySocialMedia t)
        {
            _companySocialMediaDal.Update(t);
        }
    }
}

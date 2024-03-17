using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class MainServiceManager : IMainServiceService
    {
        private readonly IMainServiceDal _mainServiceDal;

        public MainServiceManager(IMainServiceDal mainServiceDal)
        {
            _mainServiceDal = mainServiceDal;
        }
        public void TDelete(MainService t)
        {
            _mainServiceDal.Delete(t);
        }

        public MainService TGetByID(int id)
        {
            return _mainServiceDal.GetByID(id);
        }

        public List<MainService> TGetList()
        {
            return _mainServiceDal.GetList();
        }

        public void TInsert(MainService t)
        {
            _mainServiceDal.Insert(t);
        }

        public void TUpdate(MainService t)
        {
            _mainServiceDal.Update(t);
        }
    }
}

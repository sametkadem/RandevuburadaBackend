using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class DistrictManager : IDistrictService
    {
        private readonly IDistrictDal _districtDal;

        public DistrictManager(IDistrictDal districtDal)
        {
            _districtDal = districtDal;
        }

        public void TDelete(District t)
        {
            _districtDal.Delete(t);
        }

        public District TGetByID(int id)
        {
            return _districtDal.GetByID(id);
        }

        public List<District> TGetList()
        {
            return _districtDal.GetList();
        }

        public void TInsert(District t)
        {
            _districtDal.Insert(t);
        }

        public void TUpdate(District t)
        {
            _districtDal.Update(t);
        }
    }
}

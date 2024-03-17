using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class StaffWorkingPositionManager : IStaffWorkingPositionService
    {
        private readonly IStaffWorkingPositionDal _staffWorkingPositionDal;

        public StaffWorkingPositionManager(IStaffWorkingPositionDal staffWorkingPositionDal)
        {
            _staffWorkingPositionDal = staffWorkingPositionDal;
        }
        public void TDelete(StaffWorkingPosition t)
        {
            _staffWorkingPositionDal.Delete(t);
        }

        public StaffWorkingPosition TGetByID(int id)
        {
            return _staffWorkingPositionDal.GetByID(id);
        }

        public List<StaffWorkingPosition> TGetList()
        {
            return _staffWorkingPositionDal.GetList();
        }

        public void TInsert(StaffWorkingPosition t)
        {
            _staffWorkingPositionDal.Insert(t);
        }

        public void TUpdate(StaffWorkingPosition t)
        {
            _staffWorkingPositionDal.Update(t);
        }
    }
}

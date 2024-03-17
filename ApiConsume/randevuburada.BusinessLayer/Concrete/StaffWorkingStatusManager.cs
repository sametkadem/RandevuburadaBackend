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
    public class StaffWorkingStatusManager : IStaffWorkingStatusService
    {
        private readonly IStaffWorkingStatusDal _staffWorkingStatusDal;

        public StaffWorkingStatusManager(IStaffWorkingStatusDal staffWorkingStatusDal)
        {
            _staffWorkingStatusDal = staffWorkingStatusDal;
        }
        public void TDelete(StaffWorkingStatus t)
        {
            _staffWorkingStatusDal.Delete(t);
        }

        public StaffWorkingStatus TGetByID(int id)
        {
            return _staffWorkingStatusDal.GetByID(id);
        }

        public List<StaffWorkingStatus> TGetList()
        {
            return _staffWorkingStatusDal.GetList();
        }

        public void TInsert(StaffWorkingStatus t)
        {
            _staffWorkingStatusDal.Insert(t);
        }

        public void TUpdate(StaffWorkingStatus t)
        {
            _staffWorkingStatusDal.Update(t);
        }
    }
}

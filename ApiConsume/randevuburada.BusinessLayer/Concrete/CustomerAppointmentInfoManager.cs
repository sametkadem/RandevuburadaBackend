using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class CustomerAppointmentInfoManager : ICustomerAppointmentInfoService
    {
        private readonly ICustomerAppointmentInfoDal _customerAppointmentInfoDal;

        public CustomerAppointmentInfoManager(ICustomerAppointmentInfoDal customerAppointmentInfoDal)
        {
            _customerAppointmentInfoDal = customerAppointmentInfoDal;
        }

        public void TDelete(CustomerAppointmentInfo t)
        {
            _customerAppointmentInfoDal.Delete(t);
        }

        public CustomerAppointmentInfo TGetByID(int id)
        {
            return _customerAppointmentInfoDal.GetByID(id);
        }

        public List<CustomerAppointmentInfo> TGetList()
        {
            return _customerAppointmentInfoDal.GetList();
        }

        public void TInsert(CustomerAppointmentInfo t)
        {
            _customerAppointmentInfoDal.Insert(t);
        }

        public void TUpdate(CustomerAppointmentInfo t)
        {
            _customerAppointmentInfoDal.Update(t);
        }

        List<CustomerAppointmentInfo> ICustomerAppointmentInfoService.TGetByCustomerID(int customerId)
        {
            return _customerAppointmentInfoDal.GetByCustomerID(customerId);
        }
    }
}

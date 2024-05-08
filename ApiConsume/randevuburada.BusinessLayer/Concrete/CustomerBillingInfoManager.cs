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
    public class CustomerBillingInfoManager : ICustomerBillingInfoService
    {
        private readonly ICustomerBillingInfoDal _customerBillingInfoDal;

        public CustomerBillingInfoManager(ICustomerBillingInfoDal customerBillingInfoDal)
        {
            _customerBillingInfoDal = customerBillingInfoDal;
        }
        public void TDelete(CustomerBillingInfo t)
        {
            _customerBillingInfoDal.Delete(t);
        }

        public List<CustomerBillingInfo> TGetByCustomerID(int customerId)
        {
            return _customerBillingInfoDal.GetByCustomerID(customerId);
        }

        public CustomerBillingInfo TGetByID(int id)
        {
            return _customerBillingInfoDal.GetByID(id);
        }

        public List<CustomerBillingInfo> TGetList()
        {
            return _customerBillingInfoDal.GetList();
        }

        public int TGetRecordCountByCustomerID(int customerId)
        {
            return _customerBillingInfoDal.GetRecordCountByCustomerID(customerId);
        }

        public void TInsert(CustomerBillingInfo t)
        {
            _customerBillingInfoDal.Insert(t);
        }

        public void TUpdate(CustomerBillingInfo t)
        {
            _customerBillingInfoDal.Update(t);
        }
    }
}

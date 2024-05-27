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
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        //-**
        public bool TCheckCustomer(int userId)
        {
            return _customerDal.CheckCustomer(userId);
        }

        public void TDelete(Customer t)
        {
            _customerDal.Delete(t);
        }

        public Customer TGetByID(int id)
        {
            return _customerDal.GetByID(id);
        }

        public Customer TGetByUserID(int userId)
        {
            return _customerDal.GetByUserID(userId);
        }

        public List<object> TGetCustomerFirstNameLastNameAndPhoneNumbers(int customerId)
        {
            return _customerDal.GetCustomerFirstNameLastNameAndPhoneNumbers(customerId);
        }

        public string TGetCustomerName(int customerId)
        {
            return _customerDal.GetCustomerName(customerId);
        }

        public List<Customer> TGetList()
        {
            return _customerDal.GetList();
        }

        public void TInsert(Customer t)
        {
            _customerDal.Insert(t);
        }

        public void TUpdate(Customer t)
        {
            _customerDal.Update(t);
        }
    }
}

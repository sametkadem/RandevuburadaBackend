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
    public class CustomerCommentManager : ICustomerCommentService
    {
        private readonly ICustomerCommentDal _customerCommentDal;
        
        public CustomerCommentManager(ICustomerCommentDal customerCommentDal)
        {
            _customerCommentDal = customerCommentDal;
        }

        public void TDelete(CustomerComment t)
        {
            _customerCommentDal.Delete(t);
        }

        public float TGetAvgRatingByCompanyID(int companyId)
        {
            throw new NotImplementedException();
        }

        public CustomerComment TGetByID(int id)
        {
            return _customerCommentDal.GetByID(id);
        }

        public List<CustomerComment> TGetCustomerCommentByCompanyId(int companyId)
        {
            return _customerCommentDal.GetCustomerCommentByCompanyId(companyId);
        }

        public List<CustomerComment> TGetCustomerCommentByCustomerId(int customerId)
        {
            return _customerCommentDal.GetCustomerCommentByCustomerId(customerId);
        }

        public List<CustomerComment> TGetList()
        {
            return _customerCommentDal.GetList();
        }

        public void TInsert(CustomerComment t)
        {
            _customerCommentDal.Insert(t);
        }

        public void TUpdate(CustomerComment t)
        {
            _customerCommentDal.Update(t);
        }
    }
}

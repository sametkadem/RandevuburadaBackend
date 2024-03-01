using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICustomerDal : IGenericDal<Customer>
    {
        public bool CheckCustomer(int userId);
        public Customer GetByUserID(int userId);
    }
}

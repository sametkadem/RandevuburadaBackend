using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICustomerBillingInfoDal : IGenericDal<CustomerBillingInfo>
    {
        public List<CustomerBillingInfo> GetByCustomerID(int customerId);
        public int GetRecordCountByCustomerID(int customerId);

    }
}

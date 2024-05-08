using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfCustomerBillingInfoDal : GenericRepository<CustomerBillingInfo>, ICustomerBillingInfoDal
    {
        public EfCustomerBillingInfoDal(Context context) : base(context)
        {
        }

        public List<CustomerBillingInfo> GetByCustomerID(int customerId)
        {
            var context = new Context();
            return context.CustomerBillingInfo.Where(x => x.CustomerId == customerId).ToList();
        }

        public int GetRecordCountByCustomerID(int customerId)
        {
            var context = new Context();
            return context.CustomerBillingInfo.Count(x => x.CustomerId == customerId);
        }
    }
}

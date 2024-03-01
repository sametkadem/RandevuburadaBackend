using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfCustomerDal : GenericRepository<Customer>, ICustomerDal
    {
        public EfCustomerDal(Context context) : base(context)
        {

        }

        public bool CheckCustomer(int userId)
        {
            var context = new Context();
            return context.Customers.Any(x => x.UserId == userId);
        }

        public Customer GetByUserID(int userId)
        {
            var context = new Context();
            return context.Customers.FirstOrDefault(x => x.UserId == userId);
        }
    }


}

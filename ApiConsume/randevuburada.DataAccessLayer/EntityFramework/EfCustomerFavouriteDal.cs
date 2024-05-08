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
    public class EfCustomerFavouriteDal : GenericRepository<CustomerFavourite>, ICustomerFavouriteDal
    {
        public EfCustomerFavouriteDal(Context context) : base(context)
        {

        }

        public List<CustomerFavourite> GetByCustomerID(int customerId)
        {
            var context = new Context();
            return context.CustomerFavourite.Where(x => x.CustomerId == customerId).ToList();
        }

        public int GetRecordCountByCustomerID(int customerId)
        {
            var context = new Context();
            return context.CustomerFavourite.Count(x => x.CustomerId == customerId);
        }

        public bool IsExist(int customerId, int companyId)
        {
            var context = new Context();
            return context.CustomerFavourite.Any(x => x.CustomerId == customerId && x.CompanyId == companyId);
        }
    }
}

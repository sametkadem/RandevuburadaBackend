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
    public class EfCustomerCommentDal : GenericRepository<CustomerComment>, ICustomerCommentDal
    {
        public EfCustomerCommentDal(Context context) : base(context)
        {
            
        }
        public List<CustomerComment> GetCustomerCommentByCompanyId(int companyId)
        {
            var context = new Context();
            return context.CustomerComment.Where(x => x.CompanyId == companyId).ToList();
        }

        public List<CustomerComment> GetCustomerCommentByCustomerId(int customerId)
        {
            var context = new Context();
            return context.CustomerComment.Where(x => x.CustomerId == customerId).ToList();
        }

        public float GetAvgRatingByCompanyID(int companyId)
        {
            var context = new Context();
            return context.CustomerComment.Where(x => x.CompanyId == companyId).Average(x => x.Rating);
        }

    }
}

using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICustomerCommentDal : IGenericDal<CustomerComment>
    {
        public float GetAvgRatingByCompanyID(int companyId);
        public List<CustomerComment> GetCustomerCommentByCustomerId(int customerId);

        public List<CustomerComment> GetCustomerCommentByCompanyId(int companyId);
    }
}

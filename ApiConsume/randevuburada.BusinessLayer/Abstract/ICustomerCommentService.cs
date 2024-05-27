using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface ICustomerCommentService : IGenericService<CustomerComment>
    {
        public float TGetAvgRatingByCompanyID(int companyId);
        public List<CustomerComment> TGetCustomerCommentByCustomerId(int customerId);

        public List<CustomerComment> TGetCustomerCommentByCompanyId(int companyId);
    }
}

using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Abstract
{
    public interface ICustomerFavouriteService : IGenericService<CustomerFavourite>
    {
        public List<CustomerFavourite> TGetByCustomerID(int customerId);

        public int TGetRecordCountByCustomerID(int customerId);

        public bool TIsExist(int customerId, int companyId);

    }
}

using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.Abstract
{
    public interface ICustomerFavouriteDal : IGenericDal<CustomerFavourite>
    {
        public List<CustomerFavourite> GetByCustomerID(int customerId);

        public int GetRecordCountByCustomerID(int customerId);

        public bool IsExist(int customerId, int companyId);

    }
}

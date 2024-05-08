using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class CustomerFavouriteManager : ICustomerFavouriteService
    {
        private readonly ICustomerFavouriteDal _customerFavouriteDal;

        public CustomerFavouriteManager(ICustomerFavouriteDal customerFavouriteDal)
        {
            _customerFavouriteDal = customerFavouriteDal;
        }

        public void TDelete(CustomerFavourite t)
        {
            _customerFavouriteDal.Delete(t);
        }

        public List<CustomerFavourite> TGetByCustomerID(int customerId)
        {
            return _customerFavouriteDal.GetByCustomerID(customerId);
        }

        public CustomerFavourite TGetByID(int id)
        {
            return _customerFavouriteDal.GetByID(id);
        }

        public List<CustomerFavourite> TGetList()
        {
            return _customerFavouriteDal.GetList();
        }

        public int TGetRecordCountByCustomerID(int customerId)
        {
            return _customerFavouriteDal.GetRecordCountByCustomerID(customerId);
        }

        public void TInsert(CustomerFavourite t)
        {
            _customerFavouriteDal.Insert(t);
        }

        public bool TIsExist(int customerId, int companyId)
        {
            return _customerFavouriteDal.IsExist(customerId, companyId);
        }

        public void TUpdate(CustomerFavourite t)
        {
            _customerFavouriteDal.Update(t);
        }
    }
}

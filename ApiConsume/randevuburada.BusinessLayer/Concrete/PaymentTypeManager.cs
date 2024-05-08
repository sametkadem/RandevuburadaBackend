using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.EntityLayer.Concrete.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.BusinessLayer.Concrete
{
    public class PaymentTypeManager : IPaymentTypeService
    {
        private readonly IPaymentTypeDal _paymentTypeDal;

        public PaymentTypeManager(IPaymentTypeDal paymentTypeDal)
        {
            _paymentTypeDal = paymentTypeDal;
        }
        public void TDelete(PaymentType t)
        {
            _paymentTypeDal.Delete(t);
        }

        public PaymentType TGetByID(int id)
        {
            return _paymentTypeDal.GetByID(id);
        }

        public List<PaymentType> TGetList()
        {
            return _paymentTypeDal.GetList();
        }

        public void TInsert(PaymentType t)
        {
            _paymentTypeDal.Insert(t);
        }

        public void TUpdate(PaymentType t)
        {
            _paymentTypeDal.Update(t);
        }
    }
}

using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.Repositories;
using randevuburada.EntityLayer.Concrete.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DataAccessLayer.EntityFramework
{
    public class EfPaymentTypeDal : GenericRepository<PaymentType>, IPaymentTypeDal
    {
        public EfPaymentTypeDal(Context context) : base(context)
        {

        }
    }
}

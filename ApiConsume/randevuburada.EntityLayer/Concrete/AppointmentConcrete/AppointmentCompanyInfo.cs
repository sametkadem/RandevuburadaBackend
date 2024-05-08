using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.AppointmentConcrete
{
    public class AppointmentCompanyInfo
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public GeneralAppointment Appointment { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public float TotalAmount { get; set; }
        public float TotalDiscount { get; set; }
        public int TaxRate { get; set; }
        public float TaxAmount { get; set; }
        public float TotalPrice { get; set; }
        public bool IsComplate { get; set; }
        public bool IsCancel { get; set; }
        public bool IsWithdrawalAllowed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}

using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CustomerConcrete
{
    public class CustomerComment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int AppoinmentId { get; set; }
        public GeneralAppointment Appoinment { get; set; }
        public bool SystemApproved { get; set; }
        public float Rating { get; set; }
        public DateTime CommentDate { get; set; }
        public bool HideUserName { get; set; }
        public string Comment { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

    }
}

using randevuburada.EntityLayer.Concrete.AppointmentConcrete;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.ChatConcrete
{
    public class Chat
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int ChatStatusId { get; set; }
        public ChatStatus ChatStatus { get; set; }
        public DateTime LastMessageDate { get; set; }
        public DateTime ChatStartDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}

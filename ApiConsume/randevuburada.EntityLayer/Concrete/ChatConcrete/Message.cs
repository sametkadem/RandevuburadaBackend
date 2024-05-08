using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.ChatConcrete
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageDate { get; set; }
        public DateTime? MessageReadDate { get; set; }
        public bool IsRead { get; set; }
        public bool IsSystemMessage { get; set; }
        public bool IsCustomerMessage { get; set; }
        public bool IsCompanyMessage { get; set; }
        public bool IsCustomerRead { get; set; }
        public bool IsCompanyRead { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}

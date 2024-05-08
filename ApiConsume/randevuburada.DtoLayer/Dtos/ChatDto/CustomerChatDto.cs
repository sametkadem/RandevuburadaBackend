using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.ChatDto
{
    public class CustomerChatDto
    {
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }
        public string MessageText { get; set; }
    }
}

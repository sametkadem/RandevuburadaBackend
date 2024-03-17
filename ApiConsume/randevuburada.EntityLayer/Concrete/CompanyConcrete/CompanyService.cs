using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using randevuburada.EntityLayer.Concrete.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CompanyConcrete
{
    public class CompanyService
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int MainServiceId { get; set; }
        public MainService MainService { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}

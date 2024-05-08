using Microsoft.EntityFrameworkCore;
using randevuburada.EntityLayer.Concrete.CompanyConcrete.Service;
using randevuburada.EntityLayer.Concrete.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CompanyConcrete
{
    public class CompanyService
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public required List<int> CompanyStaffIds { get; set; }
        public List<CompanyStaff> CompanyStaffs { get; set; }
        public int MainServiceId { get; set; }
        [ForeignKey("MainServiceId")]
        public MainService MainService { get; set; }
        public required string ServiceName { get; set; }
        public required string ServiceDescription { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}

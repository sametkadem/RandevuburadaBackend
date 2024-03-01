using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.EntityLayer.Concrete.CompanyConcrete
{
    public class CompanyBankingDetails
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNumber { get; set; }
        public string Iban { get; set; }
        public string SwiftCode { get; set; }
        public string AccountHolder { get; set; }
        public string IbanNumber { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public ICollection<Company> Companies { get; set; }

    }
}

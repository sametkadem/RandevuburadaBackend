using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanyBankingDetailDto
{
    public class CompanyBankingDetailDtoAdd
    {
        [Required(ErrorMessage = "Banka Adı Gereklidir.")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Şube Adı Gereklidir.")]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "Hesap Numarası Gereklidir.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Iban Gereklidir.")]
        public string Iban { get; set; }

        [Required(ErrorMessage = "Hesap Sahibi Gereklidir.")]
        public string AccountHolder { get; set; }

        [Required(ErrorMessage = "Iban Numarası Gereklidir.")]
        public string IbanNumber { get; set; }

        [Required(ErrorMessage = "Vergi Numarası Gereklidir.")]
        public string TaxNumber { get; set; }

        [Required(ErrorMessage = "Vergi Dairesi Gereklidir.")]
        public string TaxOffice { get; set; }

        public string SwiftCode { get; set; }
        public int userId { get; set; }
    }
}

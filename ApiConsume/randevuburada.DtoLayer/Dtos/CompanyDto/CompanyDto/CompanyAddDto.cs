using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanyDto
{
    public class CompanyAddDto
    {
        [Required(ErrorMessage = "İşletme Tipi Gereklidir : CompanyTypeId")]
        public int CompanyTypeId { get; set; }

        [Required(ErrorMessage = "Paket Gereklidir : CompanyPackagesId")]
        public int CompanyPackagesId { get; set; }

        [Required(ErrorMessage = "Banka Detayı Gereklidir : CompanyBankingDetailsId")]
        public int CompanyBankingDetailsId { get; set; }

        [Required(ErrorMessage = "Son Kullanma Tarihi Gereklidir : ExpirationDate")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Kullanıcı Gereklidir : UserId")]
        public int UserId { get; set; }
    }
}

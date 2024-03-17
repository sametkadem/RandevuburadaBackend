using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanySubscribeDto
{
    public class CompanySubscribeUpdateDto
    {
        [Required(ErrorMessage = "Id gereklidir.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Kullanıcı Id gereklidir.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "İşletme Paket Id gereklidir.")]
        public int companyPackageId { get; set; }
        [Required(ErrorMessage = "Paket Bitiş Tarihi gereklidir.")]
        public DateTime ExpirationDate { get; set; }
    }
}

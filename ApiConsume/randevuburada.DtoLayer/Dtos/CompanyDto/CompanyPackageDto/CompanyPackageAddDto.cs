using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanyPackageDto
{
    public class CompanyPackageAddDto
    {
        [Required(ErrorMessage = "Paket Adı gereklidir.")]
        public string PackageName { get; set; }
        [Required(ErrorMessage = "Max Sms gereklidir.")]
        public int MaxSms { get; set; }
        [Required(ErrorMessage = "Paket Günü gereklidir.")]
        public int PackageDay { get; set; }
        [Required(ErrorMessage = "Max Şube gereklidir.")]
        public int MaxBranch { get; set; }
        [Required(ErrorMessage = "Paket Fiyatı gereklidir.")]
        public int PackagePrice { get; set; }
    }
}

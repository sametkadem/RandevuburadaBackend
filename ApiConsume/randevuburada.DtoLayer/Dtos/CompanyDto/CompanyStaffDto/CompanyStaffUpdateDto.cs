using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CompanyDto.CompanyStaffDto
{
    public class CompanyStaffUpdateDto
    {
        [Required(ErrorMessage = "Id gereklidir.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Şirket Kimliği gereklidir.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Çalışma Pozisyonu gereklidir.")]
        public int StaffWorkingPositionId { get; set; }

        [Required(ErrorMessage = "Çalışma Durumu gereklidir.")]
        public int StaffWorkingStatusId { get; set; }

        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Telefon Numarası gereklidir.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Tc kimlik numarası gereklidir.")]
        public string Tc { get; set; }

        [Required(ErrorMessage = "Doğum Tarihi gereklidir.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Cinsiyet gereklidir.")]
        public bool Gender { get; set; }
    }
}

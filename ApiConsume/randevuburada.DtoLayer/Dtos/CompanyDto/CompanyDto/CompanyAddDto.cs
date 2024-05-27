using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using randevuburada.EntityLayer.Concrete.Location;
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
        [Required(ErrorMessage = "İşletme türü gereklidir.")]
        public int CompanyTypeId { get; set; }

        [Required(ErrorMessage = "Banka detayı gereklidir.")]
        public int CompanyBankingDetailsId { get; set; }

        [Required(ErrorMessage = "Kullanıcı Gereklidir : UserId")]
        public int UserId { get; set; }
        public string CompanyLogo { get; set; }

        [Required(ErrorMessage = "İşletme adı gereklidir.")]
        public required string CompanyName { get; set; }
        
        [Required(ErrorMessage = "İşletme telefon numarası gereklidir.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email alanı boş geçilemez")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public required string Email { get; set; }

        [Url(ErrorMessage = "Geçerli bir website adresi giriniz")]
        public string? Website { get; set; }

        [Required(ErrorMessage = "Ülke alanı boş geçilemez")]
        [Range(1, 1, ErrorMessage = "Geçerli bir CountryId giriniz")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Şehir alanı boş geçilemez")]
        [Range(1, 81, ErrorMessage = "Geçerli bir CityId giriniz")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "İlçe alanı boş geçilemez")]
        public int DistrictId { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        [Required(ErrorMessage = "Engelli erişilebilirlik durumu gereklidir.")]
        public bool IsDisabledAccessiblity { get; set; }
        public string? CompanyAbout { get; set; }
        public string? CompanyAdress { get; set; }

        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public string? DistrictName { get; set; }

    }
}

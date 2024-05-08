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

        [Required(ErrorMessage = "İşletme adı gereklidir.")]
        public required string CompanyName { get; set; }
        
        [Required(ErrorMessage = "İşletme telefon numarası gereklidir.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Telefon numarası 11 haneli olmalıdır.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefon numarası yalnızca rakamlardan oluşmalıdır.")] 
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
        [Range(1, 39, ErrorMessage = "Geçerli bir DistrictId giriniz")]
        public int DistrictId { get; set; }

        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Geçerli bir Latitude giriniz")]
        public string? Latitude { get; set; }

        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Geçerli bir Longitude giriniz")]
        public string? Longitude { get; set; }

        [Required(ErrorMessage = "Engelli erişilebilirlik durumu gereklidir.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Geçerli bir IsDisabledAccessiblity giriniz")]
        public bool IsDisabledAccessiblity { get; set; }
        public string? CompanyAbout { get; set; }
    }
}

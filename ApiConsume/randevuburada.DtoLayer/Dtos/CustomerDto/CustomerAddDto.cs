using randevuburada.EntityLayer.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CustomerDto
{
    public class CustomerAddDto
    {
        [Required(ErrorMessage = "Ad gereklidir.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Telefon Numarası gereklidir.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Telefon Numarası 11 haneli olmalıdır.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefon Numarası yalnızca rakamlardan oluşmalıdır.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Ülke gereklidir.")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Şehir gereklidir.")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "İlçe gereklidir.")]
        public int DistrictId { get; set; }
        [Required(ErrorMessage = "Adres gereklidir.")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Cinsiyet gereklidir.")] //TRUE ERKEK FALSE KADIN
        public bool gender { get; set; }
        
        [Required(ErrorMessage = "Türkiye Cumhuriyeti Kimlik Bilgisi gereklidir.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Türkiye Cumhuriyeti Kimlik Bilgisi 11 haneli olmalıdır.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Türkiye Cumhuriyeti Kimlik Bilgisi yalnızca rakamlardan oluşmalıdır.")]
        public string Tc { get; set; }
        [Required(ErrorMessage = "Kullanıcı ID gereklidir.")]
        public int UserId { get; set; }
    }
}

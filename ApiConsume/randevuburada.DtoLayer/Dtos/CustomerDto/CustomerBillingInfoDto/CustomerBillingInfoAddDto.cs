using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Location;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randevuburada.DtoLayer.Dtos.CustomerDto.CustomerBillingInfoDto
{
    public class CustomerBillingInfoAddDto
    {
        [Required(ErrorMessage = "Müşteri Id gereklidir.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Fatura Müşteri Adı gereklidir.")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Fatura Müşteri Soyadı gereklidir.")]
        public string CustomerSurname { get; set; }
        [Required(ErrorMessage = "Fatura Kayıt Adı gereklidir.")]
        public string BillingName { get; set; }
        [Required(ErrorMessage = "Fatura Soyadı gereklidir.")]
        public string BillingAddress { get; set; }
        [Required(ErrorMessage = "Ülke gereklidir.")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Şehir gereklidir.")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "İlçe gereklidir.")]
        public int DistrictId { get; set; }
        public string? BillingZipCode { get; set; }
        public string? BillingPhone { get; set; }
        public string? BillingTcNo { get; set; }
        public string? BillingCompanyName { get; set; }
        public string? BillingTaxNo { get; set; }
        public string? BillingTaxOffice { get; set; }
        [Required(ErrorMessage = "Fatura Tipi gereklidir.")]
        public bool Commercial { get; set; }
    }
}

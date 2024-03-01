using AutoMapper;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyPackageDto;
using randevuburada.DtoLayer.Dtos.CustomerDto;
using randevuburada.DtoLayer.Dtos.IdentityDto.RegisterDto;
using randevuburada.EntityLayer.Concrete.CompanyConcrete;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Identity;

namespace HotelProject.WebApi.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
         
            CreateMap<CreateNewUserDto, AppUser>().ReverseMap();
           
            CreateMap<Customer, CustomerAddDto>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDto>().ReverseMap();

            CreateMap<CompanyPackage, CompanyPackageAddDto>().ReverseMap();


        }
    }
}

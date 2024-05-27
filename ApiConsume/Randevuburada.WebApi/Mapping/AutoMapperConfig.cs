using AutoMapper;
using randevuburada.DtoLayer.Dtos.AppointmentDto.CustomerAppointmentInfoDto;
using randevuburada.DtoLayer.Dtos.ChatDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyBankingDetailDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyOwnerInfoDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyPackageDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyServiceDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyStaffDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanySubscribeDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyTypeDto;
using randevuburada.DtoLayer.Dtos.CompanyDto.CompanyWorkingHoursDto;
using randevuburada.DtoLayer.Dtos.CustomerDto;
using randevuburada.DtoLayer.Dtos.CustomerDto.CustomerBillingInfoDto;
using randevuburada.DtoLayer.Dtos.CustomerDto.CustomerCommentDto;
using randevuburada.DtoLayer.Dtos.CustomerDto.CustomerFavouriteDto;
using randevuburada.DtoLayer.Dtos.IdentityDto.RegisterDto;
using randevuburada.EntityLayer.Concrete.ChatConcrete;
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

            CreateMap<CompanyType, CompanyTypeAddDto>().ReverseMap();

            CreateMap<CompanySubscribe, CompanySubscribeAddDto>().ReverseMap();
            CreateMap<CompanySubscribe, CompanySubscribeUpdateDto>().ReverseMap();

            CreateMap<CompanyOwnerInfo, CompanyOwnerInfoAddDto>().ReverseMap();
            CreateMap<CompanyOwnerInfo, CompanyOwnerInfoUpdateDto>().ReverseMap();

            CreateMap<CompanyBankingDetails, CompanyBankingDetailDtoAdd>().ReverseMap();
            CreateMap<CompanyBankingDetails, CompanyBankingDetailDtoUpdate>().ReverseMap();

            CreateMap<CompanyStaff, CompanyStaffAddDto>().ReverseMap();
            CreateMap<CompanyStaff, CompanyStaffUpdateDto>().ReverseMap();

            CreateMap<CompanyService, CompanyServiceAddDto>().ReverseMap();
            CreateMap<CompanyService, CompanyServiceUpdateDto>().ReverseMap();

            CreateMap<Company, CompanyAddDto>().ReverseMap();
            CreateMap<Company, CompanyUpdateDto>().ReverseMap();

            CreateMap<CompanyWorkingHours, CompanyWorkingHoursAddDto>().ReverseMap();
           

            CreateMap<CompanyWorkingHours, CompanyWorkingHoursUpdateDto>().ReverseMap();

            CreateMap<CustomerAppointmentInfo, CustomerAppointmentInfoAddDto>().ReverseMap();
            CreateMap<CustomerAppointmentInfo, CustomerAppointmentInfoUpdateDto>().ReverseMap();

            CreateMap<CustomerBillingInfo, CustomerBillingInfoAddDto>().ReverseMap();
            CreateMap<CustomerBillingInfo, CustomerBillingInfoUpdateDto>().ReverseMap();

            CreateMap<CustomerFavourite, CustomerFavouriteAddDto>().ReverseMap();

            CreateMap<Chat, CustomerChatDto>().ReverseMap();
            CreateMap<Message, CustomerChatDto>().ReverseMap();

            CreateMap<CustomerComment, CustomerCommentAddDto>().ReverseMap();

        }
    }
}

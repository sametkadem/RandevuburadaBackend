using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.BusinessLayer.Concrete;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DataAccessLayer.Concrete;
using randevuburada.DataAccessLayer.EntityFramework;
using randevuburada.EntityLayer.Concrete.Identity;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var baseUrl = builder.Configuration.GetValue<string>("BaseUrl");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// DbContext ve diðer servis kayýtlarý
builder.Services.AddDbContext<Context>();
builder.Services.AddScoped<ICityDal, EfCityDal>();
builder.Services.AddScoped<ICityService, CityManager>();

builder.Services.AddScoped<ICountryDal, EfCountryDal>();
builder.Services.AddScoped<ICountryService, CountryManager>();

builder.Services.AddScoped<IDistrictDal, EfDistrictDal>();
builder.Services.AddScoped<IDistrictService, DistrictManager>();

builder.Services.AddScoped<ICustomerDal, EfCustomerDal>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();

builder.Services.AddScoped<ICustomerDal, EfCustomerDal>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();

builder.Services.AddScoped<ICompanyDal, EfCompanyDal>();
builder.Services.AddScoped<ICompanyService, CompanyManager>();

builder.Services.AddScoped<ICompanyPackageDal, EfCompanyPackageDal>();
builder.Services.AddScoped<ICompanyPackageService, CompanyPackageManager>();

builder.Services.AddScoped<ICompanyBankingDetailsDal,  EfCompanyBankingDetailsDal>();
builder.Services.AddScoped<ICompanyBankingDetailsService, CompanyBankingDetailsManager>();

builder.Services.AddScoped<ICompanyTypeDal, EfCompanyTypeDal>();
builder.Services.AddScoped<ICompanyTypeService, CompanyTypeManager>();

builder.Services.AddScoped<ICompanySubscribeDal, EfCompanySubscribeDal>();
builder.Services.AddScoped<ICompanySubscribeService, CompanySubscribeManager>();

builder.Services.AddScoped<ICompanyOwnerInfoDal, EfCompanyOwnerInfoDal>();
builder.Services.AddScoped<ICompanyOwnerInfoService, CompanyOwnerInfoManager>();

builder.Services.AddScoped<IDayDal, EfDayDal>();
builder.Services.AddScoped<IDayService, DayManager>();

builder.Services.AddScoped<IGenderDal, EfGenderDal>();
builder.Services.AddScoped<IGenderService, GenderManager>();

builder.Services.AddScoped<IMediaTypeDal, EfMediaTypeDal>();
builder.Services.AddScoped<IMediaTypeService, MediaTypeManager>();

builder.Services.AddScoped<IMainServiceDal, EfMainServiceDal>();
builder.Services.AddScoped<IMainServiceService, MainServiceManager>();

builder.Services.AddScoped<IServiceIntervalHoursDal, EfServiceIntervalHoursDal>();
builder.Services.AddScoped<IServiceIntervalHoursService, ServiceIntervalHoursManager>();

builder.Services.AddScoped<IStaffWorkingHoursDal, EfStaffWorkingHoursDal>();
builder.Services.AddScoped<IStaffWorkingHoursService, StaffWorkingHoursManager>();

builder.Services.AddScoped<IStaffWorkingPositionDal, EfStaffWorkingPositionDal>();
builder.Services.AddScoped<IStaffWorkingPositionService, StaffWorkingPositionManager>();

builder.Services.AddScoped<IStaffWorkingStatusDal, EfStaffWorkingStatusDal>();
builder.Services.AddScoped<IStaffWorkingStatusService, StaffWorkingStatusManager>();

builder.Services.AddScoped<ICompanyStaffDal, EfCompanyStaffDal>();
builder.Services.AddScoped<ICompanyStaffService, CompanyStaffManager>();

builder.Services.AddScoped<ICompanyServiceDal, EfCompanyServiceDal>();
builder.Services.AddScoped<ICompanyServiceService, CompanyServiceManager>();

builder.Services.AddScoped<ICompanyWorkingHoursDal, EfCompanyWorkingHoursDal>();
builder.Services.AddScoped<ICompanyWorkingHoursService, CompanyWorkingHoursManager>();

builder.Services.AddScoped<IPaymentTypeDal, EfPaymentTypeDal>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeManager>();

builder.Services.AddScoped<IAppointmentStatusDal, EfAppointmentStatusDal>();
builder.Services.AddScoped<IAppointmentStatusService, AppointmentStatusManager>();

builder.Services.AddScoped<ICustomerAppointmentInfoDal, EfCustomerAppointmentInfoDal>();
builder.Services.AddScoped<ICustomerAppointmentInfoService, CustomerAppointmentInfoManager>();

builder.Services.AddScoped<ICustomerBillingInfoDal, EfCustomerBillingInfoDal>();
builder.Services.AddScoped<ICustomerBillingInfoService, CustomerBillingInfoManager>();

builder.Services.AddScoped<ICustomerFavouriteDal, EfCustomerFavouriteDal>();
builder.Services.AddScoped<ICustomerFavouriteService, CustomerFavouriteManager>();

builder.Services.AddScoped<IChatDal, EfChatDal>();
builder.Services.AddScoped<IChatService, ChatManager>();

builder.Services.AddScoped<IMessageDal, EfMessageDal>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddScoped<IChatStatusDal, EfChatStatusDal>();
builder.Services.AddScoped<IChatStatusService, ChatStatusManager>();

builder.Services.AddAuthorization();
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<Context>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = baseUrl,
        ValidAudience = baseUrl,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8HERKMitOUjiT2HgoPh/K6BkBZfdrMsbcLDRwurBuOVBpSgm8hdKcMbihDUMFVmUm+skqkAMi8rYGUcNKnOaXi6kmWEk4nq7bLrOMw35X69lMPhxMfXAnr14nC+JNDfBq5IuVE+wty8uEAdDQALzF8fCZkBuyiGI1BQ4wF/dF76y4g4CMG+0x0FdRcDGwji7oQ8Nril9ILMifYHWLmC8nUSN5UhzDubDLpieU/RzZOKEu8IV23dgOyFoCZIKuUXMrGeAntQrKQ++JcGydKNumC7mlppkT968RS9ZPGAuVf/w3D6Jdvz/yu0WYPGEpmt37Cos6BndUhPfUh6/bazb1DuIqdbj4qMZ2/sf646dy9s=\r\n")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("RandevuburadaApiCors", opts =>
    {
        opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseCors("RandevuburadaApiCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

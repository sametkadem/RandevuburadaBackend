using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultBufferSize = 50000; // Varsayılan tampon boyutu
        options.JsonSerializerOptions.MaxDepth = 500; // Nesne derinliği limiti
        options.JsonSerializerOptions.IgnoreNullValues = true; // Null değerleri yok say
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // Özellik adlarında büyük-küçük harf duyarlılığını kaldır
    });


builder.Services.AddSwaggerGen();

// DbContext ve di�er servis kay�tlar�
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

builder.Services.AddScoped<ICompanyBankingDetailsDal, EfCompanyBankingDetailsDal>();
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

builder.Services.AddScoped<IAppointmentStatusDal, EfAppointmentStatusDal>();
builder.Services.AddScoped<IAppointmentStatusService, AppointmentStatusManager>();

builder.Services.AddScoped<IGeneralAppointmentDal, EfGeneralAppointmentDal>();
builder.Services.AddScoped<IGeneralAppointmentService, GeneralAppointmentsManager>();

builder.Services.AddScoped<IAppointmentCompanyInfoDal, EfAppointmentCompanyInfoDal>();
builder.Services.AddScoped<IAppointmentCompanyInfoService, AppointmentCompanyInfoManager>();

builder.Services.AddScoped<ICustomerAppointmentInfoDal, EfCustomerAppointmentInfoDal>();
builder.Services.AddScoped<ICustomerAppointmentInfoService, CustomerAppointmentInfoManager>();

builder.Services.AddScoped<IAppointmentInfoDal, EfAppointmentInfoDal>();
builder.Services.AddScoped<IAppointmentInfoService, AppointmentInfoManager>();

builder.Services.AddScoped<ICustomerCommentDal, EfCustomerCommentDal>();
builder.Services.AddScoped<ICustomerCommentService, CustomerCommentManager>();

builder.Services.AddAuthorization();
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddTokenProvider<EmailTokenProvider<AppUser>>("Default").AddEntityFrameworkStores<Context>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes("this_is_my_dummy_secret_very_very_password")
            ),
        ValidateIssuer = false,
        ValidateAudience = false,
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

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.IdentityDto.LoginDto;
using randevuburada.DtoLayer.Dtos.IdentityDto.RegisterDto;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using Randevuburada.WebApi.Model.AuthenticationModel;

namespace Randevuburada.WebApi.Controller
{
    [Route("api/v1/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _customerService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, ICustomerService customerService)
        {
            _customerService = customerService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("account/company/register")]
        public async Task<IActionResult> RegisterCompany(CreateNewUserDto model)
        {
            if (!ModelState.IsValid)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Kayıt başarısız!",
                    data = ModelState
                };
                return BadRequest(returnIsValid);
            }

            var userNameControl = await _userManager.FindByNameAsync(model.UserName);
            if (userNameControl != null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı zaten mevcut");
            }

            var emailControl = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (emailControl != null)
            {
                ModelState.AddModelError(string.Empty, "E-Posta adresi zaten mevcut");
            }

            var phoneNumberControl = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
            if (phoneNumberControl != null)
            {
                ModelState.AddModelError(string.Empty, "Telefon numarası zaten mevcut");
            }

            if (ModelState.ErrorCount > 0)
            {
                var returnUnique = new
                {
                    status = "error",
                    message = "Kayıt başarısız!",
                    data = ModelState
                };
                return BadRequest(returnUnique);
            }
            var appUser = new AppUser
            {
                UserTypeId = 2,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);
                var user = await _userManager.FindByEmailAsync(model.Email);
                var tokenGenerator = new Token(_configuration);
                var jwtToken = tokenGenerator.Create();

                var returnData = new
                {
                    status = 200,
                    message = "Kullanıcı kaydı başarılı!",
                    data = new
                    {
                        token = jwtToken,
                        user = new
                        {
                            userType = "Company",
                            userTypeId = user.UserTypeId,
                            userId = user.Id,
                            email = user.Email,
                            userName = user.UserName,
                            firstName = user.FirstName,
                            lastName = user.LastName
                        }
                    },
                };
                return Ok(returnData);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            var returnFailedPackage = new
            {
                status = "error",
                message = "Kayıt başarısız!",
                data = ModelState
            };

            return BadRequest(returnFailedPackage);
        }

        [HttpPost]
        [Route("account/customer/register")]
        public async Task<IActionResult> RegisterCustomer(CreateNewUserDto model)
        {
            if (!ModelState.IsValid)
            {
                var returnIsValid = new
                {
                    code = 400,
                    status = "error",
                    message = "Kayıt başarısız!",
                    data = ModelState
                };
                return BadRequest(returnIsValid);
            }

            var userNameControl = await _userManager.FindByNameAsync(model.UserName);
            if (userNameControl != null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı zaten mevcut");
            }

            var emailControl = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (emailControl != null)
            {
                ModelState.AddModelError(string.Empty, "E-Posta adresi zaten mevcut");
            }

            var phoneNumberControl = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
            if (phoneNumberControl != null)
            {
                ModelState.AddModelError(string.Empty, "Telefon numarası zaten mevcut");
            }

            if (ModelState.ErrorCount > 0)
            {
                var returnUnique = new
                {
                    code = 400,
                    status = "error",
                    message = "Kayıt başarısız!",
                    data = ModelState
                };
                return BadRequest(returnUnique);
            }

            var appUser = new AppUser
            {
                UserTypeId = 1,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);
                var user = await _userManager.FindByEmailAsync(model.Email);
                var tokenGenerator = new Token(_configuration);
                var jwtToken = tokenGenerator.Create();

                var customerData = new randevuburada.EntityLayer.Concrete.CustomerConcrete.Customer
                {
                    FirstName =  model.FirstName,
                    LastName = model.LastName,
                    Phone = model.PhoneNumber,
                    UserId = user.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _customerService.TInsert(customerData);
                var customerId = _customerService.TGetByUserID(user.Id).Id;
                               
                var returnData = new
                {
                    code = 200,
                    status = "success",
                    message = "Kullanıcı kaydı başarılı!",
                    data = new
                    {
                        token = jwtToken,
                        user = new
                        {
                            userType = "Customer",
                            userTypeId = user.UserTypeId,
                            userId = user.Id,
                            customerId = customerId,
                            email = user.Email,
                            userName = user.UserName,
                            firstName = user.FirstName,
                            lastName = user.LastName
                        }
                    },
                };
                return Ok(returnData);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            var returnFailedPackage = new
            {
                code = 400,
                status = "error",
                message = "Kayıt başarısız!",
                data = ModelState
            };

            return BadRequest(returnFailedPackage);
        }

        [HttpPost]
        [Route("account/customer/login")]
        public async Task<IActionResult> Login(LoginUserDto model)
        {
            if (!ModelState.IsValid)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Giriş başarısız!",
                    data = ModelState
                };
                return BadRequest(returnIsValid);
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Identifier)
                    ?? await _userManager.FindByNameAsync(model.Identifier)
                    ?? await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.Identifier);
                if (user != null)
                {
                    if(user.UserTypeId == 2)
                    {
                        var returnIsValid = new
                        {
                            status = "error",
                            message = "Giriş Başarısız! Yetkisiz İşlem."
                        };
                        return BadRequest(returnIsValid);
                    }
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var tokenGenerator = new Token(_configuration);
                        var jwtToken = tokenGenerator.Create();

                        var returnData = new
                        {
                            code = 200,
                            status = "success",
                            data = new
                            {
                                token = jwtToken,
                                user = new
                                {
                                    userType = "Customer",
                                    userTypeId = 1,
                                    userId = user.Id,
                                    email = user.Email,
                                    userName = user.UserName,
                                    firstName = user.FirstName,
                                    lastName = user.LastName
                                }
                            },
                        };
                        return Ok(returnData);
                    }
                }
     
            }

            var returnFailedPackage = new
            {
                status = "error",
                message = "Giriş başarısız. Lütfen giriş bilgilerinizi kontrol edin ve tekrar deneyin!"
            };
            return BadRequest(returnFailedPackage);
        }

        [HttpPost]
        [Route("account/company/login")]
        public async Task<IActionResult> LoginCompany(LoginUserDto model)
        {
            if (!ModelState.IsValid)
            {
                var returnIsValid = new
                {
                    status = "error",
                    message = "Giriş Başarısız!",
                    data = ModelState
                };
                return BadRequest(returnIsValid);
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Identifier)
                    ?? await _userManager.FindByNameAsync(model.Identifier)
                    ?? await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.Identifier);

                if (user != null)
                {
                    if (user.UserTypeId == 1)
                    {
                        var returnIsValid = new
                        {
                            status = "error",
                            message = "Giriş Başarısız! Yetkisiz İşlem."
                        };
                        return BadRequest(returnIsValid);
                    }
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var tokenGenerator = new Token(_configuration);
                        var jwtToken = tokenGenerator.CreateComponyToken();

                        var returnData = new
                        {
                            code = 200,
                            status = "success",
                            data = new
                            {
                                token = jwtToken,
                                user = new
                                {
                                    userType = "Company",
                                    userTypeId = 2,
                                    userId = user.Id,
                                    email = user.Email,
                                    userName = user.UserName,
                                    firstName = user.FirstName,
                                    lastName = user.LastName
                                }
                            },
                        };
                        return Ok(returnData);
                    }
                }
            }

            var returnFailedPackage = new
            {
                status = "error",
                message = "Giriş başarısız. Lütfen giriş bilgilerinizi kontrol edin ve tekrar deneyin!"
            };
            return BadRequest(returnFailedPackage);
        }

        [HttpGet]
        [Route("account/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Çıkış başarılı");
        }

        [HttpGet]
        [Route("account/getUser")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı");
            }
            return Ok(user);
        }

        [HttpGet]
        [Authorize]
        [Route("account/getUserId")]
        public async Task<IActionResult> GetUserId()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı");
            }
            return Ok(user.Id);
        }
    }
}
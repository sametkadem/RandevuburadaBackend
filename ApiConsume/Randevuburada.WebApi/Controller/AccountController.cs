using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using randevuburada.BusinessLayer.Abstract;
using randevuburada.DataAccessLayer.Abstract;
using randevuburada.DtoLayer.Dtos.IdentityDto.LoginDto;
using randevuburada.DtoLayer.Dtos.IdentityDto.RegisterDto;
using randevuburada.DtoLayer.Dtos.IdentityDto.ResetPasswordDto;
using randevuburada.EntityLayer.Concrete.CustomerConcrete;
using randevuburada.EntityLayer.Concrete.Identity;
using Randevuburada.WebApi.Model.AuthenticationModel;
using Randevuburada.WebApi.Model.MailModel;

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
            try
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
                    if (user == null)
                    {
                        var returnNullUserData = new
                        {
                            status = "error",
                            message = "Kullanıcı Bulunamadı!"
                        };
                        return NotFound(returnNullUserData);
                    }


                    // Kullanıcıyı doğrulama için e-posta gönder
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var userIdEncoded = Uri.EscapeDataString(appUser.Id.ToString());
                    var tokenEncoded = Uri.EscapeDataString(token);
                    var confirmationLink = $"https://backend.randevuburada.com/api/v1/account/confirm/email?userId={userIdEncoded}&token={tokenEncoded}";
                    // E-posta gönderme işlemi
                    var mailDto = new MailDto("noreply@randevuburada.com", "smtKDM110*", model.Email);
                    string mailBody = $@"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Email Doğrulama</title>
                    <style>
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                    }}
                    .button {{
                        display: inline-block;
                        background-color: #007bff;
                        color: #fff;
                        padding: 10px 20px;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    </style>
                    </head>
                    <body>
                    <div class=""container"">
                        <p>Merhaba,</p>
                        <p>Hesabınızı doğrulamak için lütfen aşağıdaki linke tıklayınız:</p>
                        <a href=""{confirmationLink}"" class=""button"">Hesabı Doğrula</a>
                    </div>
                    </body>
                    </html>
                    ";
                    mailDto.SendMail(mailBody, "Randevuburada - Mail Doğrulama");

                    var tokenGenerator = new Token(_configuration);
                    var jwtToken = tokenGenerator.CreateComponyToken();

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
            }catch(Exception ex)
            {
                var returnErrorPackage = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorPackage);
            }

        }

        [HttpPost]
        [Route("account/customer/register")]
        public async Task<IActionResult> RegisterCustomer(CreateNewUserDto model)
        {
            try
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

                    // Kullanıcıyı doğrulama için e-posta gönder
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var userIdEncoded = Uri.EscapeDataString(appUser.Id.ToString());
                    var tokenEncoded = Uri.EscapeDataString(token);
                    var confirmationLink = $"https://backend.randevuburada.com/api/v1/account/confirm/email?userId={userIdEncoded}&token={tokenEncoded}";
                    // E-posta gönderme işlemi
                    var mailDto = new MailDto("noreply@randevuburada.com", "smtKDM110*", model.Email);
                    string mailBody = $@"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Email Doğrulama</title>
                    <style>
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                    }}
                    .button {{
                        display: inline-block;
                        background-color: #007bff;
                        color: #fff;
                        padding: 10px 20px;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    </style>
                    </head>
                    <body>
                    <div class=""container"">
                        <p>Merhaba,</p>
                        <p>Hesabınızı doğrulamak için lütfen aşağıdaki linke tıklayınız:</p>
                        <a href=""{confirmationLink}"" class=""button"">Hesabı Doğrula</a>
                    </div>
                    </body>
                    </html>
                    ";
                    mailDto.SendMail(mailBody, "Randevuburada - Mail Doğrulama");


                    var customerData = new randevuburada.EntityLayer.Concrete.CustomerConcrete.Customer
                    {
                        FirstName = model.FirstName,
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
                    status = "error",
                    message = "Kayıt başarısız!",
                    data = ModelState
                };

                return BadRequest(returnFailedPackage);
            }catch(Exception ex)
            {
                var returnErrorPackage = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorPackage);
            }
           
        }

        [HttpPost]
        [Route("account/customer/login")]
        public async Task<IActionResult> Login(LoginUserDto model)
        {
            try
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
                        if (user.UserTypeId == 2)
                        {
                            var returnIsValid = new
                            {
                                status = "error",
                                message = "Giriş Başarısız! Yetkisiz İşlem."
                            };
                            return BadRequest(returnIsValid);
                        }
                        if (user.EmailConfirmed == false)
                        {
                            // Kullanıcıyı doğrulama için e-posta gönder
                            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var userIdEncoded = Uri.EscapeDataString(user.Id.ToString());
                            var tokenEncoded = Uri.EscapeDataString(token);
                            var confirmationLink = $"https://backend.randevuburada.com/api/v1/account/confirm/email?userId={userIdEncoded}&token={tokenEncoded}";
                            // E-posta gönderme işlemi
                            var mailDto = new MailDto("noreply@randevuburada.com", "smtKDM110*", user.Email);
                            string mailBody = $@"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Email Doğrulama</title>
                    <style>
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                    }}
                    .button {{
                        display: inline-block;
                        background-color: #007bff;
                        color: #fff;
                        padding: 10px 20px;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    </style>
                    </head>
                    <body>
                    <div class=""container"">
                        <p>Merhaba,</p>
                        <p>Hesabınızı doğrulamak için lütfen aşağıdaki linke tıklayınız:</p>
                        <a href=""{confirmationLink}"" class=""button"">Hesabı Doğrula</a>
                    </div>
                    </body>
                    </html>
                    ";
                            mailDto.SendMail(mailBody, "Randevuburada - Mail Doğrulama");

                            var returnIsValid = new
                            {
                                status = "error",
                                message = "Giriş Başarısız! E-Posta adresinizi doğrulamadınız. Mail adresinize doğrulama için mail iletilmiştir."
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
            }catch(Exception ex)
            {
                var returnErrorPackage = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorPackage);
            }
            
        }

        [HttpPost]
        [Route("account/company/login")]
        public async Task<IActionResult> LoginCompany(LoginUserDto model)
        {
            try
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
                            if (user.EmailConfirmed == false)
                            {
                                // Kullanıcıyı doğrulama için e-posta gönder
                                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                                var userIdEncoded = Uri.EscapeDataString(user.Id.ToString());
                                var tokenEncoded = Uri.EscapeDataString(token);
                                var confirmationLink = $"https://backend.randevuburada.com/api/v1/account/confirm/email?userId={userIdEncoded}&token={tokenEncoded}";
                                // E-posta gönderme işlemi
                                var mailDto = new MailDto("noreply@randevuburada.com", "smtKDM110*", user.Email);
                                string mailBody = $@"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Email Doğrulama</title>
                    <style>
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                    }}
                    .button {{
                        display: inline-block;
                        background-color: #007bff;
                        color: #fff;
                        padding: 10px 20px;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    </style>
                    </head>
                    <body>
                    <div class=""container"">
                        <p>Merhaba,</p>
                        <p>Hesabınızı doğrulamak için lütfen aşağıdaki linke tıklayınız:</p>
                        <a href=""{confirmationLink}"" class=""button"">Hesabı Doğrula</a>
                    </div>
                    </body>
                    </html>
                    ";
                                mailDto.SendMail(mailBody, "Randevuburada - Mail Doğrulama");

                                var returnIsValid = new
                                {
                                    status = "error",
                                    message = "Giriş Başarısız! E-Posta adresinizi doğrulamadınız. Mail adresinize doğrulama için mail iletilmiştir."
                                };
                                return BadRequest(returnIsValid);
                            }

                            var tokenGenerator = new Token(_configuration);

                            var jwtToken = tokenGenerator.CreateComponyToken();
                            Console.WriteLine(jwtToken);

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
            }catch(Exception ex)
            {
                var returnErrorPackage = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorPackage);
            }
         
        }

        [HttpGet]
        [Route("account/logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                var returnData = new
                {
                    status = "success",
                    message = "Çıkış başarılı"
                };
                return Ok(returnData);
            }catch(Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
            
        }

        [HttpGet]
        [Route("account/getUser")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    var returnNullUserData = new
                    {
                        status = "error",
                        message = "Kullanıcı bulunamadı."
                    };
                    return BadRequest(returnNullUserData);
                }
                var returnData = new
                {
                    status = "success",
                    data = new
                    {
                        user = user
                    }
                };
                return Ok(returnData);
            }
            catch (Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("account/getUserId")]
        public async Task<IActionResult> GetUserId()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    var returnNullUserData = new
                    {
                        status = "error",
                        message = "Kullanıcı bulunamadı."
                    };
                    return BadRequest(returnNullUserData);
                }
                var returnData = new
                {
                    status = "success",
                    data = new
                    {
                        userId = user.Id
                    }
                };
                return Ok(returnData);
            }catch(Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
        }

        [HttpGet("account/confirm/email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {

            try
            {
                if (userId == null || token == null)
                {
                    var returnNullData = new
                    {
                        status = "error",
                        message = "Kullanıcı kimliği veya doğrulama kodu eksik."
                    };
                    return BadRequest(returnNullData);
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    var returnNullUserData = new
                    {
                        status = "error",
                        message = "Kullanıcı bulunamadı."
                    };
                    return BadRequest(returnNullUserData);
                }

                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    var returnData = new
                    {
                        status = "success",
                        message = "Hesabınız başarıyla doğrulandı."
                    };
                    return Ok(returnData);
                }
                else
                {
                    var returnFailedData = new
                    {
                        status = "error",
                        message = "Hesabınız doğrulanamadı."
                    };
                    return BadRequest(returnFailedData);
                }
            }catch(Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
           
        }

        [HttpPost]
        [Route("account/forgot/password")]
        public async Task<IActionResult> ForgotPassword(string mail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var returnIsValid = new
                    {
                        status = "error",
                        message = "Geçersiz model",
                        data = ModelState
                    };
                    return BadRequest(returnIsValid);
                }

                var user = await _userManager.FindByEmailAsync(mail);
                if (user == null)
                {
                    var returnNullUserData = new
                    {
                        status = "error",
                        message = "Kullanıcı bulunamadı."
                    };
                    return BadRequest(returnNullUserData);
                }

        
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var userIdEncoded = Uri.EscapeDataString(user.Id.ToString());
                var tokenEncoded = Uri.EscapeDataString(token);
                var resetLink = $"https://backend.randevuburada.com/api/v1/account/reset/password?userId={userIdEncoded}&token={tokenEncoded}";
                string mailBody = $@"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Şifre Sıfırlama</title>
                    <style>
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                    }}
                    .button {{
                        display: inline-block;
                        background-color: #007bff;
                        color: #fff;
                        padding: 10px 20px;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    </style>
                    </head>
                    <body>
                    <div class=""container"">
                        <p>Merhaba,</p>
                        <p>Hesabınızın şifresini sıfırlamak için lütfen aşağıdaki linke tıklayınız:</p>
                        <a href=""{resetLink}"" class=""button"">Şifremi Sıfırla</a>
                    </div>
                    </body>
                    </html>
                    ";
                var mailDto = new MailDto("noreply@randevuburada.com", "smtKDM110*", user.Email);
                mailDto.SendMail(mailBody, "Randevuburada - Şifre Sıfırlama");
                var returnData = new
                {
                    status = "success",
                    message = "Şifre sıfırlama maili gönderildi.",
                    token = token,
                    userId = user.Id
                };
                return Ok(returnData);
            }
            catch (Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
        }

        [HttpPost]
        [Route("account/reset/password")]
        public async Task<IActionResult> ResetPassword(ResetPaswordDto resetPaswordDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    var returnIsValid = new
                    {
                        status = "error",
                        message = "İşlem başarısız!",
                        data = ModelState
                    };
                    return BadRequest(returnIsValid);
                }

                var user = await _userManager.FindByIdAsync(resetPaswordDto.userId);
                if (user == null)
                {
                    var returnNullUserData = new
                    {
                        status = "error",
                        message = "Kullanıcı bulunamadı."
                    };
                    return BadRequest(returnNullUserData);
                }

           
                var result = await _userManager.ResetPasswordAsync(user, resetPaswordDto.token, resetPaswordDto.Password);
                if (result.Succeeded)
                {
                    var returnData = new
                    {
                        status = "success",
                        message = "Şifre sıfırlama işlemi başarılı."
                    };
                    return Ok(returnData);
                }
                else
                {
                    var returnFailedData = new
                    {
                        status = "error",
                        message = "Şifre sıfırlama işlemi başarısız."
                    };
                    return BadRequest(returnFailedData);
                }
               
            }
            catch (Exception ex)
            {
                var returnErrorData = new
                {
                    status = "error",
                    message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, returnErrorData);
            }
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using randevuburada.DtoLayer.Dtos.IdentityDto.LoginDto;
using randevuburada.DtoLayer.Dtos.IdentityDto.RegisterDto;
using randevuburada.EntityLayer.Concrete.Identity;
using Randevuburada.WebApi.Model.AuthenticationModel;

namespace Randevuburada.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(CreateNewUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);
                return Ok("Kullanıcı kaydı başarılı!");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var tokenGenerator = new Token(_configuration);
                    var jwtToken = tokenGenerator.Create();

                    return Ok(jwtToken);
                }
            }
            ModelState.AddModelError(string.Empty, "Giriş başarısız");
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Çıkış başarılı");
        }

        [HttpGet]
        [Route("GetUser")]
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
        [Route("GetUserId")]
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
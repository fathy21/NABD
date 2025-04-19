using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NABD.Areas.Identity;

namespace NABD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountsController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Areas.InputModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                NationalId = model.NationalId,
                PhoneNumber = model.PhoneNumber,
                UserType = model.UserType,
                Specialty = model.UserType.ToLower() == "doctor" ? model.Specialty : null
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Auth",
                    new { userId = user.Id, token }, Request.Scheme);

                await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                    $"Please confirm your account by clicking <a href='{confirmationLink}'>here</a>.");

                if (!_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new { Message = "User registered and signed in successfully!" });
                }

                return Ok(new { Message = "User registered successfully. Please check your email to confirm your account." });
            }

            return BadRequest(result.Errors);
        }
    }
}

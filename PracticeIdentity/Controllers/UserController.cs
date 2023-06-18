using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PracticeIdentity.Data;
using PracticeIdentity.DTOS;
using PracticeIdentity.Models;

namespace PracticeIdentity.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public UserController(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IMapper mapper,
            ApplicationDbContext context
            )
        {
            // var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // optionsBuilder.UseSqlServer(connectionString);  // Replace connectionString with your actual connection string.
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return PartialView((_mapper.Map<IEnumerable<UserDTO>>(_userManager.Users).ToList()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]

        public IActionResult GetModal()
        {
            return PartialView("ShowModal", new RegisterModel());
        }

        [HttpGet]
        public async Task<IActionResult> GetModalEdit(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            var userDTO = _mapper.Map<UserDTO>(user);
            userDTO.Roles = (List<string>)await _userManager.GetRolesAsync(user);
            ViewBag.lstRoles = _roleManager.Roles.ToList();
            return PartialView("EditModal", userDTO);
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, registerModel.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, registerModel.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //Add Claims
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Email, registerModel.Email));


                    await _userManager.AddClaimsAsync(user, claims);
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var user = await _userManager.FindByEmailAsync(userDTO.Email);

                    user.PhoneNumber = userDTO.PhoneNumber;
                    var result = await _userManager.UpdateAsync(user);

                    if (userDTO.Roles.Count > 0)
                    {
                        var checkRoles = await _userManager.GetRolesAsync(user);
                        if (checkRoles.Count > 0)
                        {
                            await _userManager.RemoveFromRolesAsync(user, (IEnumerable<string>)checkRoles);
                            // _userManager.UpdateAsync
                        }

                        var roleResult = await _userManager.AddToRolesAsync(user, userDTO.Roles);
                    }
                    transaction.Commit();
                }

                // user = _mapper.Map<IdentityUser>(userDTO);
            }
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.DeleteAsync(user);
            return Content("Ok");
        }
    }
}
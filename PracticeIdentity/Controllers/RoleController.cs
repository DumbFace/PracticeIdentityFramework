using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticeIdentity.Data;
using PracticeIdentity.DTOS;
using PracticeIdentity.Utils;

namespace PracticeIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ApplicationDbContext _context;

        public RoleController(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper,
            ApplicationDbContext context
            )
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            return PartialView((_mapper.Map<IEnumerable<RoleModel>>(_roleManager.Roles.ToList())));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]

        public async Task<IActionResult> GetModal(string name = "")
        {
            var role = await _roleManager.FindByNameAsync(name);
            return PartialView("ShowModal", (_mapper.Map<RoleModel>(role)));
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleModel obj)
        {
            if (ModelState.IsValid)
            {
                var hasRole = await _roleManager.RoleExistsAsync(obj.Name);
                if (!hasRole)
                {
                    await _roleManager.CreateAsync(new IdentityRole(obj.Name));
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update()
        {
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            await _roleManager.DeleteAsync(role);
            return Content("Ok");
        }


        [HttpGet]
        public async Task<IActionResult> GetModalPermission(string name)
        {
            ViewBag.role = name;

            IdentityRole role = await _roleManager.FindByNameAsync(name);

            var roleClaims = await _roleManager.GetClaimsAsync(role);
            List<string> claimsAsString = new List<string>();

            foreach (Claim claim in roleClaims)
            {
                claimsAsString.Add(claim.Value);
            }

            ViewBag.lstPermission = Util.GeneratePermissions();
            return PartialView("ModalPermission", claimsAsString);
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePermission(List<string> permissions, string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            var allClaims = await _roleManager.GetClaimsAsync(role);

            //Remove all claims first
            using (var transaction = _context.Database.BeginTransaction())
            {
                foreach (var claim in allClaims)
                {
                    var testResult = await _roleManager.RemoveClaimAsync(role, claim);
                }
                transaction.Commit();
            }

            var allPermissions = permissions;
            foreach (var permission in allPermissions)
            {
                // if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))https://localhost:7020/Role#
                // {
                //     var result = await _roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                // }
                var result = await _roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }

            return RedirectToAction("Index");
        }
    }
}
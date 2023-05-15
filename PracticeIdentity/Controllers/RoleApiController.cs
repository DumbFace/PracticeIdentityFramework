using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PracticeIdentity.Controllers
{

    public class Role {
        public string Name { get; set; }
    }
    [ApiController]
    [Route("api/role")]
    public class RoleApiController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleApiController(
            UserManager<IdentityUser> _usermgr,
            RoleManager<IdentityRole> _rolemgr
        )
        {
            _userManager = _usermgr;
            _roleManager = _rolemgr;
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Role role)
        {
            if (!string.IsNullOrEmpty(role.Name))
            {
                if (!(await _roleManager.RoleExistsAsync(role.Name)))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role.Name));
                    return Ok();
                }
                else
                {
                    return BadRequest("Roles Exist");
                }
            }
            else
            {
                return BadRequest("Input Roles");
            }
        }
    }
}
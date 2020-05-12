using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Quickstart.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager = null;
        private readonly RoleManager<IdentityRole> _roleManager = null;

        public UserManagerController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("createuser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromForm] UserDTO userDTO)
        {
            IdentityUser user = new IdentityUser();
            user.Email = userDTO.Email;
            user.UserName = userDTO.UserName;
            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if(result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Age", userDTO.Age.ToString()));
                return Ok();
            }
           
            return BadRequest(result.Errors);
        }


        [HttpPost("CreateRole")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRoleAsync([FromForm] RoleDTO roleDTO)
        {

            IdentityRole role = new IdentityRole();
            role.Name = roleDTO.RoleName;
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("AddUserRole")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUserRoleAsync([FromForm] UserRoleDTO userRoleDTO)
        {

          var user=  await _userManager.FindByNameAsync(userRoleDTO.UserName);

           var result= await _userManager.AddToRoleAsync(user, userRoleDTO.RoleName);

            if (result.Succeeded)
            {
               
                return Ok();
            }

            return BadRequest();
        }
    }

    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public int Age { get; set; }
    }
    public class RoleDTO
    {
        public string RoleName { get; set; }
    }

    public class UserRoleDTO
    {
        public string UserName { get; set; }

        public string RoleName { get; set; }

    }
}
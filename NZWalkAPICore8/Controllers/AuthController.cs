using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPICore8.CustomActionFilter;
using NZWalkAPICore8.Model.DTO;
using NZWalkAPICore8.Repositaries;

namespace NZWalkAPICore8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepositary tokenRepositary;
        public AuthController(UserManager<IdentityUser> _userManager, ITokenRepositary _tokenRepositary)
        {
            this.userManager = _userManager;
            tokenRepositary = _tokenRepositary;
        }

        //Post ://api/Auth/Register/
        [HttpPost]
        [Route("Register")]
        [ValidateModel]
        public  async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };
            var userCreated =  await this.userManager.CreateAsync(identityUser,registerRequestDTO.Password);
            if(userCreated.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                   
                    var identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Created Sucessfully. you can login");
                    }
                }
            }
            return BadRequest("Some thing worng!");

        }

        //Post /api/Auth/Login
        [HttpPost]
        [ValidateModel]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
           var identityUser =  await userManager.FindByEmailAsync(loginRequestDTO.UserName);
            if(identityUser != null)
            {
                var result =await userManager.CheckPasswordAsync(identityUser, loginRequestDTO.Password);
                if(result)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);
                    //Generate Token
                    string token = tokenRepositary.GenerateJWTToken(identityUser, roles.ToList());
                    var loginResponseDTO = new LoginResponseDTO
                    {
                        JwtToken = token
                    };
                    return Ok(loginResponseDTO);
                }
            }
            return BadRequest("UserName or Password Is Invalid");
        }
     }
}

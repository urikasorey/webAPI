using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using libaryAPI.Repository;
using libaryAPI.Models.DTO;
namespace libaryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ITokenRepository _tokenRepository;
		public UserController(UserManager<IdentityUser> userManager, ITokenRepository
		tokenRepository)
		{
			_userManager = userManager;
			_tokenRepository = tokenRepository;
		}
		//POST:/api/Auth/Register - chức năng đăng ký user
		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDTO
		registerRequestDTO) // khai báo kiểu model cho Register
		{
			var identityUser = new IdentityUser
			{
				UserName = registerRequestDTO.UserName,
				Email = registerRequestDTO.UserName
			};
			var identityResult = await _userManager.CreateAsync(identityUser,
			registerRequestDTO.Password);
			if (identityResult.Succeeded)
			{
				//add roles to this user
				if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
				{
					identityResult = await _userManager.AddToRolesAsync(identityUser,
					registerRequestDTO.Roles);
				}
				if (identityResult.Succeeded)
				{
					return Ok("Register Successful! Let login!");
				}
			}
			return BadRequest("Something wrong!");
		}// end action Register
		 //POST:/api/Auth/Login - chức năng đăng nhập user
		

	}// end class user controller
}



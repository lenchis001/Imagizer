using AutoMapper;
using Imagizer.BLL.Services;
using Imagizer.Controllers.API.V1;
using Imagizer.Models.API.V1;
using Imagizer.Models.API.V1.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Imagizer.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class UserController : Controller
	{
		private readonly IUserService _userService;
		private readonly IHashService _hashService;
		private readonly IMapper _mapper;

		public UserController(
			IUserService userService,
			IHashService hashService,
			IMapper mapper)
		{
			_userService = userService;
			_hashService = hashService;
			_mapper = mapper;
		}


		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<ActionResult<User>> Register([FromBody] AddUserData model)
		{
			var bllUser = _mapper.Map<BLL.Models.Entities.Users.AddUserData>(model);
			var bllRegisterResult = await _userService.AddAsync(bllUser);
			var registerResult = _mapper.Map<DefaultDataFetchResult<User>>(bllRegisterResult);

			var result = registerResult.Error;
			if (registerResult.Error == ErrorCode.OK)
			{
				var signInResult = await MakeSignIn(user: registerResult.Data);
				result = signInResult.Error;
			}

			return this.ToActionResult(result, registerResult.Data);
		}

		[AllowAnonymous]
		[HttpPost("signIn")]
		public async Task<ActionResult<User>> SignIn([FromBody] SignInData body)
		{
			var loginProcessResult = await MakeSignIn(body.Email, body.Password);

			var result = _mapper.Map<DefaultDataFetchResult<User>>(loginProcessResult);
			return this.ToActionResult(result.Error, result.Data);
		}

		[HttpGet("isSignedIn")]
		[Authorize]
		public void IsSignedIn() { }

		private async Task<DefaultDataFetchResult<User>> MakeSignIn(string email, string password)
		{
			var passwordHash = _hashService.MakeHash(password);

			var result = _mapper.Map<User>(await _userService.GetByCredsAsync(email, passwordHash));

			return await MakeSignIn(result);
		}

		private async Task<DefaultDataFetchResult<User>> MakeSignIn(User user)
		{
			if (user != null)
			{
				List<Claim> claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.Email),
					new Claim(ClaimTypes.Role, "User"),
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
				};

				ClaimsIdentity claimsIdentity = new ClaimsIdentity(
					claims, CookieAuthenticationDefaults.AuthenticationScheme);

				AuthenticationProperties authProperties = new AuthenticationProperties
				{
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					authProperties);

				return new DefaultDataFetchResult<User>
				{
					Error = ErrorCode.OK,
					Data = _mapper.Map<User>(user)
				};
			}
			else
			{
				return new DefaultDataFetchResult<User>
				{
					Error = ErrorCode.ACCESS_DENIED
				};
			}
		}
	}
}

using AutoMapper;
using BankingControlPanel_Models;
using BankingControlPanel_Models.Dtos;
using BankingControlPanel_Models.Models;
using BankingControlPanel_Models.ValidateModelAttributes;
using BankingControlPanel_Utilities.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankingControlPanel.Controllers
{

    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper,
            TokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [ValidateModel]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Ok(ResponseModel.Failure("Unauthorized user", 401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);


            if (result.Succeeded) return Ok(ResponseModel.Seccuss(await CreateUserObject(user), ""));


            return Ok(ResponseModel.Failure("Unauthorized user", 401));
        }



        [HttpPost("register")]
        [ValidateModel]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
                return Ok(ResponseModel.Failure("Enter another email", 400));

            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName))
                return Ok(ResponseModel.Failure("Enter another username", 400));


            var user = _mapper.Map<ApplicationUser>(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            //only for the first run of the project is needed
            //await _roleManager.CreateAsync(new ApplicationRole(SD.AdminRole));
            //await _roleManager.CreateAsync(new ApplicationRole(SD.UserRole));

            // in the consumer application (1- type must not be entered by the user 2- type will be sent depending on the register user role)
            IdentityResult roleResult;
            
            if (registerDto.Type==SD.AdminRole) 
            {
                 roleResult = await _userManager.AddToRoleAsync(user, SD.AdminRole);
            }
            else if (registerDto.Type == SD.UserRole)
            {
                 roleResult = await _userManager.AddToRoleAsync(user, SD.UserRole);
            }
            else
            {
                return Ok(ResponseModel.Failure("role name is invalid ", 400));
            }


            if (result.Succeeded && roleResult.Succeeded)
                return Ok(ResponseModel.Seccuss(await CreateUserObject(user), "Registration successful"));

            return Ok(ResponseModel.Failure("Failur in the registeration process", 400));
        }


        // creates returned user object for response json
        private async Task<UserDto> CreateUserObject(ApplicationUser user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = await _tokenService.CreateToken(user);
            return userDto;
        }
    }
}

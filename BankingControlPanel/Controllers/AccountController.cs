using AutoMapper;
using BankingControlPanel_Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Controllers
{

    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
       // private readonly TokenService _tokenService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper
            //TokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
           // _tokenService = tokenService;
        }


        //register
        //login
    }
}

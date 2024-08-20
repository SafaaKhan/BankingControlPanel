using AutoMapper;
using BankingControlPanel_DataAccess.Repositories.IRepositories;
using BankingControlPanel_Models;
using BankingControlPanel_Models.Dtos;
using BankingControlPanel_Models.Models;
using BankingControlPanel_Models.Pagination;
using BankingControlPanel_Models.ValidateModelAttributes;
using BankingControlPanel_Utilities.Helpers;
using BankingControlPanel_Utilities.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingControlPanel.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = SD.AdminRole)]
    public class ClientsController : BaseApiController
    {
        private readonly IClientRepo _clientRepo;
        public ClientsController(IClientRepo clientRepo)
        {
            _clientRepo = clientRepo;
        }


        [HttpPost("addClient")]
        [ValidateModel]
        public async Task<IActionResult> AddClient([FromBody] ClientDto clientDto)
        {
            return Ok(await _clientRepo.AddClientAsync(clientDto));
        }


        [HttpGet("getClients")]
        public async Task<IActionResult> GetClients([FromQuery] UserParams userParams)
        {
            var clients = await _clientRepo.GetClientsAsync(userParams);
            Response.AddPagination(clients.CurrentPage, clients.PageSize, clients.TotalCount, clients.TotalPages);
            var searchParams = new
            {
                clients.TotalCount,
                clients.PageSize,
                clients.CurrentPage,
                clients.TotalPages,
                userParams.Sex,
                userParams.City
            };

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _clientRepo.AddSearchParamsLogAsync(userId,JsonSerializer.Serialize(searchParams));

            return Ok(clients);
        }


        [HttpGet("getLastSearchParams")]
        public async Task<IActionResult> GetLastSearchParams([FromQuery]UserParams userParams)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // to get search params for the logged user
            var searchedParams = await _clientRepo.GetLastSearchParamsAsync(userId, userParams.ParamsNum);
          
            var deserializedSearchedParams = searchedParams
                .Select(param => JsonSerializer.Deserialize<object>(param)) //deserialize object to remove '\' from searchedParams result
                .ToList();

            return Ok(deserializedSearchedParams);

        }
    }
}

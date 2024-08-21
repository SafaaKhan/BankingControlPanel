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


        /// <summary>
        /// Add the client to the system by admin user.
        /// (must sent the token (admin user) to avoid unauthorized access)
        /// </summary>
        /// <param name="clientDto">The client data transfer object (clientDto).</param>
        /// <returns></returns>
        /// <response code="200"> A response indicating the success along with client has been added (clientDto)</response>
        /// <response code="400" > A response indicating the failure with isSeccuss= false in Response Model </response>
        /// <response code="500">If there is an internal server error with isSeccuss= false in Response Model </response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel))]
        [HttpPost("addClient")]
        [ValidateModel]
        public async Task<IActionResult> AddClient([FromBody] ClientDto clientDto)
        {
            return Ok(await _clientRepo.AddClientAsync(clientDto));
        }


        /// <summary>
        /// Get clients with (pagination, filtering, sorting => pass 'desc'/'asc' in orderBy()) params.
        /// (must sent the token (admin user) to avoid unauthorized access)
        /// </summary>
        /// <param name="userParams"></param>
        /// <returns></returns>
        /// <response code="200"> A response indicating the success for getting users</response>
        /// <response code="400" > A response indicating the failure with isSeccuss= false in Response Model </response>
        /// <response code="500">If there is an internal server error with isSeccuss= false in Response Model </response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<ClientDto>))]
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


        /// <summary>
        /// Get the last search parameters.
        /// (must sent the token (admin user) to avoid unauthorized access)
        /// </summary>
        /// <param name="userParams"> Pass the required number of parameters in ParamsNum</param>
        /// <returns></returns>
        /// <response code="200"> A response indicating the success for getting last searched parameters</response>
        /// <response code="400" > A response indicating the failure with isSeccuss= false in Response Model </response>
        /// <response code="500">If there is an internal server error with isSeccuss= false in Response Model </response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<object>))]
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

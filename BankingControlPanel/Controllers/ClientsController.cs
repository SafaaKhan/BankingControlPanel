using AutoMapper;
using BankingControlPanel_DataAccess.Repositories.IRepositories;
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
            //must make sure there is at least one accoutn
            return Ok(await _clientRepo.AddClientAsync(clientDto));
        }


    }
}

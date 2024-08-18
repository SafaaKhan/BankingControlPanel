using BankingControlPanel_Models.Dtos;
using BankingControlPanel_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingControlPanel_DataAccess.Repositories.IRepositories
{
    public interface IClientRepo
    {
        //add client 

        // getClients with paging (userparams)
        Task<ResponseModel> GetClientsAsync();
        Task<ResponseModel> AddClientAsync(ClientDto clientDto);
    }
}

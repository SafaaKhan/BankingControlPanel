﻿using BankingControlPanel_Models.Dtos;
using BankingControlPanel_Models.Models;
using BankingControlPanel_Models.Pagination;
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
        Task<PagedList<ClientDto>> GetClientsAsync(UserParams userParams);
        Task<ResponseModel> AddClientAsync(ClientDto clientDto);
        Task AddSearchParamsLogAsync(string userId,string searchParams);
    }
}

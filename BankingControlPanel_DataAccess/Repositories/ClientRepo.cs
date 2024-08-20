using AutoMapper;
using AutoMapper.QueryableExtensions;
using BankingControlPanel_DataAccess.Data;
using BankingControlPanel_DataAccess.Repositories.IRepositories;
using BankingControlPanel_Models.Dtos;
using BankingControlPanel_Models.Models;
using BankingControlPanel_Models.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankingControlPanel_DataAccess.Repositories
{
    public class ClientRepo : IClientRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ClientRepo(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ResponseModel> AddClientAsync(ClientDto clientDto)
        {
            if(clientDto.Accounts.Count==0)
            {
                return ResponseModel.Failure("At least one account is required.", 500);

            }

            var client = _mapper.Map<Client>(clientDto);

            _db.Clients.Add(client);

            int result = await _db.SaveChangesAsync();
            if (result<=0)
            {
                return ResponseModel.Failure("Failed to create the activity", 500);
            }

            return ResponseModel.Seccuss(_mapper.Map<ClientDto>(client), "");
        }

        public async Task AddSearchParamsLogAsync(string userId,string searchParams)
        {
            _db.SearchParamsLogs.Add(new SearchParamsLog { UserId = userId, Parameters = searchParams });
            await _db.SaveChangesAsync();
        }

        public async Task<PagedList<ClientDto>> GetClientsAsync(UserParams userParams)
        {
            var query = _db.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(userParams.Sex))
                query=query.Where(x => x.Sex == userParams.Sex);
            if (!string.IsNullOrEmpty(userParams.City))
                query = query.Where(x => x.Address.City == userParams.City);

            return await PagedList<ClientDto>.CreateAsync(query.ProjectTo<ClientDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
        }

        public async Task<List<string>> GetLastSearchParamsAsync(string userId,int paramsNum)
        {
            
            return await  _db.SearchParamsLogs.Where(x=>x.UserId== userId).
                OrderByDescending(x => x.CreatedAt).Take(paramsNum).
                Select(x=>x.Parameters).ToListAsync();
        }
    }
}

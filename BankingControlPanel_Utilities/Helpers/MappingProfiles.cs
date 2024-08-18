using AutoMapper;
using BankingControlPanel_Models.Dtos;
using BankingControlPanel_Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingControlPanel_Utilities.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<RegisterDto, UserDto>().ReverseMap();
            CreateMap<ApplicationUser, RegisterDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using Zattini.Application.DTOs;
using Zattini.Domain.Entities;

namespace Zattini.Application.Mappings
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping()
        {
            CreateMap<UserDTO, User>();
            CreateMap<UserAddressDTO, UserAddress>();
        }
    }
}

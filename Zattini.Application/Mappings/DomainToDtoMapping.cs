using AutoMapper;
using Zattini.Application.DTOs;
using Zattini.Domain.Entities;

namespace Zattini.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<User, UserDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))
                ));

            CreateMap<UserAddress, UserAddressDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))
                ));
        }
    }
}

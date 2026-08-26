using AutoMapper;
using Ecommerce.Application.Dtos.Auth;
using Ecommerce.Domain.Models.Orders;


namespace Ecommerce.Application.AutoMapperProfiles
{
    internal class AddressProfile: Profile
    {
        public AddressProfile() 
        {
            CreateMap<AddressDto, OrderAddress>();
       
        
        
        }

    }
}

using AutoMapper;
using Weather.Services.CartApi.Dtos.CartDetails;
using Weather.Services.CartApi.Dtos.CartHeaders;
using Weather.Services.CartApi.Models;

namespace Weather.Services.CartApi.Extensioins;

public static class MappingConfig
{

    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
            config.CreateMap<CartDetailsDto, CartDetail>().ReverseMap();
        
            config.CreateMap<CartAddHeaderDto, CartHeader>().ReverseMap();
            config.CreateMap<CartAddDetailsDto, CartDetail>().ReverseMap();

            config.CreateMap<CartUpdateDetailsDto, CartDetail>().ReverseMap();

            config.CreateMap<CartUpdateHeaderStatusDto, CartDetail>().ReverseMap();

        });

        return mappingConfig;
    }
}

using AutoMapper;
using Store.Services.CartApi.Dto;
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
        });

        return mappingConfig;
    }
}

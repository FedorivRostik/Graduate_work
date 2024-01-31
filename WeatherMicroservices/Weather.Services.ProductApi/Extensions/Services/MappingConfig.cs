using AutoMapper;
using Weather.Services.ProductApi.Dtos.Genres;
using Weather.Services.ProductApi.Dtos.Products;
using Weather.Services.ProductApi.Models;

namespace Weather.Services.ProductApi.Extensioins;

public static class MappingConfig
{

    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Product, ProductDto>()
            .ForMember(vm => vm.GenreName, m => m
                .MapFrom(u => u.Genre != null ? u.Genre.Name : string.Empty));

            config.CreateMap<ProductDto, Product>();
            config.CreateMap<ProductCreateDto, Product>().ReverseMap();

            config.CreateMap<GenreDto, Genre>().ReverseMap();
            config.CreateMap<GenreCreateDto, Genre>().ReverseMap();
        });

        return mappingConfig;
    }
}

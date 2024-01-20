using AutoMapper;
using Weather.Services.ProductApi.Dtos.Products;
using Weather.Services.ProductApi.Models;

namespace Weather.Services.ProductApi.Extensioins;

public class MappingConfig
{
	public static MapperConfiguration RegisterMaps()
	{
		var mappingConfig = new MapperConfiguration(config =>
		{
			config.CreateMap<ProductDto, Product>().ReverseMap();
			config.CreateMap<ProductCreateDto, Product>().ReverseMap();
		});

		return mappingConfig;
	}
}

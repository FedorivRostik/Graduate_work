using AutoMapper;

namespace Weather.Services.AuthApi.Extensioins;

public class MappingConfig
{
	public static MapperConfiguration RegisterMaps()
	{
		var mappingConfig = new MapperConfiguration(config =>
		{
			
		});

		return mappingConfig;
	}
}

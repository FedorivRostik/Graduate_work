using AutoMapper;

using Weather.Services.NuclearApi;

namespace Weather.Services.NuclearApi.Extensioins;

public static class MappingConfig
{

    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
          
        });

        return mappingConfig;
    }
}

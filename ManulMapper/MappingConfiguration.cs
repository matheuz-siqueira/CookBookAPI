using cookbook_api.Dtos.Recipe;
using cookbook_api.Dtos.User;
using cookbook_api.Models;
using HashidsNet;
using Mapster;

namespace cookbook_api.Interfaces;

public static class MappingConfiguration
{
    public static void Register(this IServiceCollection services)
    {
        TypeAdapterConfig<Recipe, GetAllResponse>
        .NewConfig()
        .Map(destiny => destiny.Quantity, config => config.Ingredients.Count);
    }
}

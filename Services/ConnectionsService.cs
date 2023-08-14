using System.Runtime.CompilerServices;
using System.Security.Claims;
using cookbook_api.Dtos.User;
using cookbook_api.Repositories;
using Mapster;


namespace cookbook_api.Services;

public class ConnectionsService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ConnectionRepository _connectionRepository;
    private readonly UserRepository _userRepository;
    private readonly RecipeRepository _recipeRepository;

    public ConnectionsService(IHttpContextAccessor httpContextAccessor,
        ConnectionRepository connectionRepository,
        UserRepository userRepository,
        RecipeRepository recipeRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _connectionRepository = connectionRepository;
        _userRepository = userRepository;
        _recipeRepository = recipeRepository;
    }
    public async Task<IList<UserConnectedResponse>> GetConnection()
    {
        var userId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var connections = await _connectionRepository.GetConnectionsAsync(userId);
        var size = connections.Count();
        var list = new List<Models.User>(size);
        for (int i = 0; i < size; i++)
        {
            var user = new Models.User();
            user = await _userRepository.GetById(connections[i]);
            list.Add(user);
        }

        var tasks = list.Select(async user =>
        {
            var recipeQuantity = await _recipeRepository.RecipeCountAsync(user.Id);

            var item = user.Adapt<UserConnectedResponse>();
            item.Quantity = recipeQuantity;

            return item;
        });
        return await Task.WhenAll(tasks);
    }
}

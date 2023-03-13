using System.Net.Http.Headers;
using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;

namespace OneClass.WebAPI.Services;

public class UserService : IUserService
{
    private readonly IAccessTokenService _accessTokenService;
    public UserService(IAccessTokenService accessTokenService)
        => _accessTokenService = accessTokenService;

    public async Task<UserData> GetAuthenticatedUserAsync(
        HttpContext context,
        CancellationToken cancellationToken = default
    )
    {
        var token = _accessTokenService.GetAccessToken(context);
        if (token is null)
        {
            throw new Exception("Unauthorized");
        }
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer", token);
        var response = await httpClient.GetAsync(
            "https://graph.microsoft.com/v1.0/me",
            cancellationToken
        );
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Bad Request");
        }
        var me = await response.Content.ReadFromJsonAsync<Me>(cancellationToken: cancellationToken);
        if (me is null)
        {
            throw new Exception("Bad Request");
        }
        return UserData.FromMe(me);
    }
}
using System.Net.Http.Headers;
using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;

namespace OneClass.WebAPI.Services;

public class UserService : IUserService
{
    public async Task<UserData> GetAuthenticatedUserAsync(
        HttpContext context,
        CancellationToken cancellationToken = default
    )
    {
        var token = context.Request.Headers.Authorization[0];
        if (token is null)
        {
            throw new Exception("Unauthorized");
        }
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token.Replace("Bearer", "")
        );
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

using System.Net.Http.Headers;
using Marten;
using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;

namespace OneClass.WebAPI.Services;

public class UserService : IUserService
{
    private readonly IDocumentSession _session;

    public UserService(IAccessTokenService accessTokenService, IDocumentSession session)
    {
        _session = session;
    }

    public async Task<UserData> GetAuthenticatedUserAsync(
        string accessToken,
        CancellationToken cancellationToken = default
    )
    {
        if (accessToken is null)
        {
            throw new Exception("Unauthorized");
        }
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            accessToken
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

        var user = await _session
            .Query<UserData>()
            .FirstOrDefaultAsync(x => x.Id == me.Id, cancellationToken);

        return user ?? UserData.FromMe(me);
    }
}

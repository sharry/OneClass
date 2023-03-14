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
        var client = new GraphServiceClientProvider()
            .GetGraphServiceClient(accessToken);
        var response = await client.Me.GetAsync(cancellationToken: cancellationToken);
        if (response is null)
        {
            throw new Exception("Bad Request");
        }
        var me = response;
        var user = await _session
            .Query<UserData>()
            .FirstOrDefaultAsync(x => x.Id == me.Id, cancellationToken);

        return user ?? UserData.FromGraphUser(me);
    }
}

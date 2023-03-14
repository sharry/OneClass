using Azure.Core;
using Microsoft.Graph;

namespace OneClass.WebAPI.Services;

public class GraphServiceClientProvider : IGraphServiceClientProvider
{
	public GraphServiceClient GetGraphServiceClient(string accessToken)
	{
		return new GraphServiceClient(
			new AccessTokenCredential(accessToken),
			new string[]
			{ 
				"User.Read",
                "User.ReadBasic.All",
                "Tasks.ReadWrite",
                "Calendars.ReadWrite",
                "Mail.ReadWrite",
                "Files.ReadWrite",
                "Files.ReadWrite.All" 
			}
		);
	}
}

public class AccessTokenCredential : TokenCredential
{
	private readonly AccessToken _accessToken;
	public AccessTokenCredential(string accessToken)
	{
		_accessToken = new AccessToken(accessToken, DateTimeOffset.MaxValue);
	}
	public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
	{
		return new ValueTask<AccessToken>(_accessToken);
	}
	public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
	{
		return _accessToken;
	}
}
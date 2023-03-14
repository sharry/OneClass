using System.Net.Http.Headers;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Authentication;
using Microsoft.Graph.Core;
using Microsoft.Kiota.Abstractions.Authentication;

namespace OneClass.WebAPI.Services;

public class GraphServiceClientProvider : IGraphServiceClientProvider
{
	public GraphServiceClient GetGraphServiceClient(string accessToken)
	{
		return new GraphServiceClient(new BaseBearerTokenAuthenticationProvider(
			new AzureIdentityAccessTokenProvider(
				new DefaultAzureCredential()
			)
		));
	}
}
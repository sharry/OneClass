using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace OneClass.MAUIApp.Services
{
	internal class GraphService
	{
		private readonly string[] _scopes = new[] { "User.Read" };
		private const string _clientId = "896a45d1-095f-416d-baf3-ff19493fc2c1";
		private const string _tenantId = "dc0474a7-98b7-41c5-9561-bbef81b01215";
		private GraphServiceClient _client;

		public GraphService()
		{
			Initialize();
		}

		private void Initialize()
		{
			if (OperatingSystem.IsWindows())
			{
				var options = new InteractiveBrowserCredentialOptions
				{
					ClientId = _clientId,
					TenantId = _tenantId,
					AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
					RedirectUri = new Uri("https://login.microsoftonline.com/common/oauth2/nativeclient"),
				};

				InteractiveBrowserCredential interactiveCredential = new(options);
				_client = new GraphServiceClient(interactiveCredential, _scopes);
			}
			else
			{
				// TODO: Add iOS/Android support
			}


		}

		public async Task<User> GetMyDetailsAsync()
		{
			try
			{
				return await _client.Me.GetAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error loading user details: {ex}");
				return null;
			}
		}
	}
}
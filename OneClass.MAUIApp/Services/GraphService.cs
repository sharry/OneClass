using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace OneClass.MAUIApp.Services
{
	internal class GraphService
	{
		private readonly string[] _scopes = new[] { "User.Read" };
		private const string _clientId = "9a17e3bc-872d-461d-b92b-458828b5eb3d";
		private const string _tenantId = "84c31ca0-ac3b-4eae-ad11-519d80233e6f";
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
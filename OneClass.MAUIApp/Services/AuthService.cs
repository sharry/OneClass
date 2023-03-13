using Microsoft.Identity.Client;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace OneClass.MAUIApp.Services;
public sealed class AuthService
{
	public IPublicClientApplication IdentityClient { get; set; }
	public async Task<AuthResult> GetAuthenticationToken()
	{
		if (IdentityClient is null)
		{
#if ANDROID
		IdentityClient = PublicClientApplicationBuilder
			.Create(Constants.ClientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithRedirectUri($"msal{Constants.ClientId}://auth")
            .WithParentActivityOrWindow(() => Platform.CurrentActivity)
            .Build();
#elif IOS
        IdentityClient = PublicClientApplicationBuilder
            .Create(Constants.ClientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
            .WithRedirectUri($"msal{Constants.ClientId}://auth")
            .Build();
#else
		IdentityClient = PublicClientApplicationBuilder
			.Create(Constants.ClientId)
			.WithAuthority(AzureCloudInstance.AzurePublic, "common")
			.WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
			.Build();
#endif
		}
		var accounts = await IdentityClient.GetAccountsAsync();
		AuthenticationResult result = null;
		bool tryInteractiveLogin = false;
		try
		{
			result = await IdentityClient
				.AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault())
				.ExecuteAsync();
		}
		catch (MsalUiRequiredException)
		{
			tryInteractiveLogin = true;
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"MSAL Silent Error: {ex.Message}");
		}
		if (tryInteractiveLogin)
		{
			try
			{
				result = await IdentityClient
					.AcquireTokenInteractive(Constants.Scopes)
					.ExecuteAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"MSAL Interactive Error: {ex.Message}");
			}
		}
		if (result is null)
		{
			return default;
		}
		var idToken = new JwtSecurityToken(result.IdToken);
		return new AuthResult(result.AccessToken, idToken.Payload.Exp.Value);
	}
}

public sealed record AuthResult(
	string AccessToken,
	int Exp
);
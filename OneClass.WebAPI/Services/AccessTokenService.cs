namespace OneClass.WebAPI.Services;

public class AccessTokenService : IAccessTokenService
{
	public string GetAccessToken(HttpContext context)
	{
		var token = context.Request.Headers.Authorization[0];
		if (token is null)
		{
			throw new Exception("Unauthorized");
		}
		return token.Replace("Bearer", "");
	}
}
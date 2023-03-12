namespace OneClass.WebAPI.Services;

public interface IAccessTokenService
{
	public string GetAccessToken(HttpContext context);
}
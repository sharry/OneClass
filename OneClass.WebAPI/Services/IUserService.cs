using OneClass.Domain.DbModels;

namespace OneClass.WebAPI.Services;

public interface IUserService
{
	public Task<UserData> GetAuthenticatedUserAsync(
		HttpContext context,
		CancellationToken cancellationToken
	);
}
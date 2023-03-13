using OneClass.Domain.DbModels;

namespace OneClass.WebAPI.Services;

public interface IUserService
{
	public Task<UserData> GetAuthenticatedUserAsync(
		string accessToken,
		CancellationToken cancellationToken = default
	);
}
using Carter;
using Marten;
using OneClass.WebAPI.Services;

namespace OneClass.WebAPI.User;

public class UserModule : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/users/me", 
			async (
				IDocumentSession session,
				HttpContext context,
				IUserService userService,
				CancellationToken cancellationToken) =>
		{
			var user = await userService.GetAuthenticatedUserAsync(context, cancellationToken);
			return Results.Ok(user);
		});
	}
}
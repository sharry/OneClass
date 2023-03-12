using Carter;
using Marten;
using OneClass.Domain.DbModels;
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
			UserData user;
			try
			{
				user = await userService.GetAuthenticatedUserAsync(context, cancellationToken);
			}
			catch (Exception e)
			{
				return Results.Unauthorized();
			}
			var existingUser = await session
				.Query<UserData>()
				.FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken);
			if (existingUser is not null)
			{
				return Results.Ok(existingUser); 
			}
			session.Store(user);
			await session.SaveChangesAsync(cancellationToken);
			return Results.Ok(user);
		});
	}
}
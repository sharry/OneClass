using Carter;
using Marten;
using OneClass.Domain.DbModels;
using OneClass.WebAPI.Services;

namespace OneClass.WebAPI.Classroom;

public class ClassroomModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/classrooms",
            async (
                IDocumentSession session,
                HttpContext context,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var user = await userService.GetAuthenticatedUserAsync(context, cancellationToken);
                return Results.Ok(user);
            }
        );

        app.MapGet(
            "/api/classrooms/{id}",
            (string id) =>
            {
                return Results.Ok($"Classroom {id}");
            }
        );

        app.MapPost(
            "/api/classrooms",
            (ClassRoomData classRoomData) =>
            {
                return Results.Ok(classRoomData);
            }
        );

        app.MapPut(
            "/api/classrooms/{id}",
            (string id, ClassRoomData classRoomData) =>
            {
                return Results.Ok(classRoomData);
            }
        );

        app.MapDelete(
            "/api/classrooms/{id}",
            (string id) =>
            {
                return Results.Ok($"Classroom {id} deleted");
            }
        );
    }
}

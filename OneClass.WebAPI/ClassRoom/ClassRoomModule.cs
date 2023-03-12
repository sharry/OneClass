using Carter;
using Marten;
using OneClass.Domain.DbModels;
using OneClass.WebAPI.Services;

namespace OneClass.WebAPI.ClassRoom;

public class ClassRoomModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/api/classrooms",
            async (
                IDocumentSession session,
                HttpContext context,
                CancellationToken cancellationToken
            ) =>
            {
                UserData user;
                user = await UserServices.GetAuthenticatedUserAsync(context, cancellationToken);

                // try
                // {
                // }
                // catch (System.Exception)
                // {
                //     return Results.Unauthorized();
                // }

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

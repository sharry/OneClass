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
                var classrooms = await session
                    .Query<ClassRoomData>()
                    .Where(x => x.TeacherId == user.Id || x.StudentIds.Any(id => id == user.Id))
                    .ToListAsync(cancellationToken);

                return Results.Ok(classrooms);
            }
        );

        app.MapGet(
            "/api/classrooms/{id}",
            async (
                string id,
                IDocumentSession session,
                HttpContext context,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var user = await userService.GetAuthenticatedUserAsync(context, cancellationToken);
                var classroom = await session
                    .Query<ClassRoomData>()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id && !classroom.StudentIds.Any(x => x == user.Id))
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(classroom);
            }
        );

        app.MapPost(
            "/api/classrooms",
            (
                ClassRoomData classRoomData,
                IDocumentSession session,
                HttpContext context,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var user = userService.GetAuthenticatedUserAsync(context, cancellationToken).Result;
                classRoomData.Id = Guid.NewGuid().ToString();
                classRoomData.TeacherId = user.Id;
                classRoomData.StudentIds = new string[0];

                session.Store(classRoomData);
                session.SaveChanges();
                return Results.Ok(classRoomData);
            }
        );

        app.MapPut(
            "/api/classrooms/{id}",
            (
                string id,
                ClassRoomData classRoomData,
                IDocumentSession session,
                HttpContext context,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var user = userService.GetAuthenticatedUserAsync(context, cancellationToken).Result;
                var classroom = session.Query<ClassRoomData>().FirstOrDefault(x => x.Id == id);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id)
                {
                    return Results.Unauthorized();
                }

                classroom.Title = classRoomData.Title;
                classroom.Description = classRoomData.Description;
                classroom.Image = classRoomData.Image;

                session.Store(classroom);
                session.SaveChanges();

                return Results.Ok(classroom);
            }
        );

        app.MapDelete(
            "/api/classrooms/{id}",
            (
                string id,
                IDocumentSession session,
                HttpContext context,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var user = userService.GetAuthenticatedUserAsync(context, cancellationToken).Result;
                var classroom = session.Query<ClassRoomData>().FirstOrDefault(x => x.Id == id);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id)
                {
                    return Results.Unauthorized();
                }

                session.Delete(classroom);
                session.SaveChanges();

                return Results.Ok();
            }
        );
    }
}

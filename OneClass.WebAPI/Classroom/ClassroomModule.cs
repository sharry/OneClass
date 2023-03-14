using Carter;
using Marten;
using Microsoft.AspNetCore.Mvc;
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
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
                var classrooms = await session
                    .Query<ClassroomData>()
                    .ToListAsync(cancellationToken);
                var userClassrooms = classrooms
                    .Where(x => x.TeacherId == user.Id || x.StudentIds.Any(y => y == user.Id))
                    .ToList();
                return Results.Ok(userClassrooms);
            }
        );

        app.MapGet(
            "/api/classrooms/{id}",
            async (
                string id,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
                var classroom = await session
                    .Query<ClassroomData>()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id && classroom.StudentIds.All(x => x != user.Id))
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(classroom);
            }
        );

        app.MapPost(
            "/api/classrooms",
            async (
                ClassroomData classRoomData,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken,
                IOneDriveService oneDriveService
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
                classRoomData.Id = Guid.NewGuid().ToString();
                classRoomData.TeacherId = user.Id;
                classRoomData.StudentIds = Array.Empty<string>();
                classRoomData.JoinCode = Guid.NewGuid().ToString("N")[..6];

                var folder = oneDriveService.CreateClassroomFolderAsync(context, classRoomData, cancellationToken).Result;
                classRoomData.OneDriveFolderId = folder.Id;

                user.JoinClass(classRoomData.Id, "Teacher");

                session.Store(classRoomData);
                session.Store(user);
                await session.SaveChangesAsync(cancellationToken);


                return Results.Ok(classRoomData);
            }
        );

        app.MapPut(
            "/api/classrooms/{id}",
            async (
                string id,
                ClassroomData classRoomData,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
                var classroom = session.Query<ClassroomData>().FirstOrDefault(x => x.Id == id);

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
                await session.SaveChangesAsync(cancellationToken);

                return Results.Ok(classroom);
            }
        );

        app.MapDelete(
            "/api/classrooms/{id}",
            async (
                string id,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(accessToken, cancellationToken);
                var classroom = session.Query<ClassroomData>().FirstOrDefault(x => x.Id == id);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.TeacherId != user.Id)
                {
                    return Results.Unauthorized();
                }

                session.Delete(classroom);
                await session.SaveChangesAsync(cancellationToken);

                return Results.Ok();
            }
        );

        app.MapPost(
            "/api/classrooms/join",
            async (
                [FromBody] dynamic request,
                IDocumentSession session,
                HttpContext context,
                IAccessTokenService atService,
                IUserService userService,
                ITodoService todoService,
                CancellationToken cancellationToken
            ) =>
            {
                var token = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(token, cancellationToken);
                string joinCode = request.GetProperty("code").GetString();
                var classroom = session
                    .Query<ClassroomData>()
                    .FirstOrDefault(x => x.JoinCode == joinCode);

                if (classroom is null)
                {
                    return Results.NotFound();
                }

                if (classroom.StudentIds.Any(x => x == user.Id))
                {
                    return Results.Ok(classroom);
                }

                classroom.StudentIds = classroom.StudentIds.Append(user.Id).ToArray();
                session.Store(classroom);

                var todoList = await todoService.CreateTodoListAsync(context, $"OneClass : {classroom.Title}", cancellationToken);
                user.JoinClass(classroom.Id, "Student");
                user.AddTodoList(classroom.Id, todoList.Id);
                session.Store(user);

                await session.SaveChangesAsync(cancellationToken);

                return Results.Ok(user);
            }
        );
    }
}

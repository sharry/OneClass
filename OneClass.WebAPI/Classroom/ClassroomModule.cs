using Carter;
using Marten;
using Microsoft.AspNetCore.Mvc;
using OneClass.Domain.DbModels;
using OneClass.Domain.Utils;
using OneClass.WebAPI.Services;

namespace OneClass.WebAPI.Classroom;

public class ClassroomModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapGet("/dummy", (
            IDocumentSession session
        ) => {
            // Delete all data in the database
            session.DeleteWhere<ClassroomData>(x => true);
            session.DeleteWhere<ResourceData>(x => true);
            session.DeleteWhere<ClassroomAssignment>(x => true);

            var userIds = new List<string>();
            userIds.Add("9504c03bb803bbc4"); // Youssef
            userIds.Add("d2a0e7927b7d719e"); // Elbachir
            userIds.Add("954da67e6be3e94f"); // Abderrazaq
            userIds.Add("12f00991b3f9f4ae"); // Brahim

            // Create classrooms for users
            for (int i = 0; i < 10; i++)
            {
                var classroom = new ClassroomData();
                classroom.Id = Guid.NewGuid().ToString();
                classroom.Title = $"Classroom {i}";
                classroom.Description = $"Classroom {i} description";
                classroom.Image = $"image-0{new Random().Next(1, 5)}.svg";
                classroom.JoinCode = Guid.NewGuid().ToString().Substring(0, 6);
                classroom.TeacherId = userIds[new Random().Next(0, 4)];
                classroom.StudentIds = Array.Empty<string>();
                session.Store(classroom);
            }

            // Create resources for classrooms
            var classrooms = session.Query<ClassroomData>().ToList();
            for (int i = 0; i < 20; i++)
            {
                var classroom = classrooms[new Random().Next(0, 10)];
                var teacher = session.Query<UserData>().FirstOrDefault(x => x.Id == classroom.TeacherId);
                var resource = new ResourceData();
                resource.Id = Guid.NewGuid().ToString();
                resource.Content = $"Resource {i} Content";
                resource.Teacher = new Teacher
                {
                    Id = classroom.TeacherId,
                    Name = teacher?.DisplayName ?? "Teacher Name",
                };
                resource.ClassroomId = classroom.Id;
                resource.CreatedAt = DateTime.UtcNow.ToIso8601String();
                resource.Attachments = Array.Empty<AttachedFile>();
                session.Store(resource);
            }

            // Create assignments for classrooms
            for (int i = 0; i < 20; i++)
            {
                var classroom = classrooms[new Random().Next(0, 10)];
                var assignment = new ClassroomAssignment(classroom.TeacherId, classroom.Id);
                assignment.Id = Guid.NewGuid().ToString();
                assignment.Title = $"Assignment {i}";
                assignment.Content = $"Assignment {i} Content";
                assignment.HasDueDate = new Random().Next(0, 2) == 1;
                assignment.DueDate = DateTime.UtcNow.AddDays(new Random().Next(1, 10)).ToIso8601String();
                assignment.AttachedFiles = Array.Empty<AttachedFile>();
                session.Store(assignment);
            }

            // Add students to classes
            var classes = session.Query<ClassroomData>().ToList();
            foreach (var classroom in classes)
            {
                var students = session.Query<UserData>().Where(x => x.Id != classroom.TeacherId).ToList();
                var studentsCount = new Random().Next(1, 5);
                for (int i = 0; i < studentsCount; i++)
                {
                    var student = students[new Random().Next(0, students.Count)];
                    classroom.StudentIds = classroom.StudentIds.Append(student.Id).ToArray();
                    session.Store(classroom);
                    session.Store(student);
                }
            }

            session.SaveChanges();

            return Results.Ok();
        });

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
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );
                var classrooms = await session
                    .Query<ClassroomData>()
                    .ToListAsync(cancellationToken);
                var userClassrooms = classrooms
                    .Where(x => x.TeacherId == user.Id || x.StudentIds.Any(y => y == user.Id))
                    .ToList();
               List<ClassroomResponse> classroomResponses = new();
                foreach (var classroom in userClassrooms)
                {
                    var teacher = await session
                        .Query<UserData>()
                        .FirstOrDefaultAsync(x => x.Id == classroom.TeacherId, cancellationToken);
                    if (teacher is null)
                    {
                        return Results.NotFound();
                    }
                    var assignmentsCount = await session
                        .Query<ClassroomAssignment>()
                        .Where(x => x.ClassroomId == classroom.Id)
                        .CountAsync(cancellationToken);
                    classroomResponses.Add(new ClassroomResponse(
                        classroom.Id,
                        classroom.Title,
                        classroom.Description,
                        classroom.Image,
                        new TeacherResponse(
                            teacher.Id,
                            teacher.GivenName,
                            teacher.Surname,
                            teacher.DisplayName,
                            teacher.EmailAddress
                        ),
                        classroom.StudentIds,
                        classroom.JoinCode,
                        classroom.OneDriveFolderId,
                        assignmentsCount
                    ));
                }
                return Results.Ok(classroomResponses);
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
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );
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
                var teacher = await session
                    .Query<UserData>()
                    .FirstOrDefaultAsync(x => x.Id == classroom.TeacherId, cancellationToken);
                var assignmentsCount = await session
                    .Query<ClassroomAssignment>()
                    .Where(x => x.ClassroomId == classroom.Id)
                    .CountAsync(cancellationToken);
                if (teacher is null)
                {
                    return Results.NotFound();
                }
                var classRoomResponse = new ClassroomResponse(
                    classroom.Id,
                    classroom.Title,
                    classroom.Description,
                    classroom.Image,
                    new TeacherResponse(
                        teacher.Id,
                        teacher.GivenName,
                        teacher.Surname,
                        teacher.DisplayName,
                        teacher.EmailAddress
                    ),
                    classroom.StudentIds,
                    classroom.JoinCode,
                    classroom.OneDriveFolderId,
                    assignmentsCount
                );
                return Results.Ok(classRoomResponse);
            }
        );

        app.MapGet(
            "/api/classrooms/{id}/members",
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
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );
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

                var teacher = await session
                    .Query<UserData>()
                    .FirstOrDefaultAsync(x => x.Id == classroom.TeacherId, cancellationToken);
                var students = await session
                    .Query<UserData>()
                    .Where(x => x.JoinedClasses.Any(y => y.ClassroomId == classroom.Id))
                    .ToListAsync(cancellationToken);

                return Results.Ok(new { teacher, students });
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
                IDriveService oneDriveService
            ) =>
            {
                var accessToken = atService.GetAccessToken(context);
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );
                classRoomData.Id = Guid.NewGuid().ToString();
                classRoomData.TeacherId = user.Id;
                classRoomData.StudentIds = Array.Empty<string>();
                classRoomData.JoinCode = Guid.NewGuid().ToString("N")[..6];

                var folder = oneDriveService
                    .CreateClassroomFolderAsync(context, classRoomData, cancellationToken)
                    .Result;
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
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );
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
                var user = await userService.GetAuthenticatedUserAsync(
                    accessToken,
                    cancellationToken
                );
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

                var todoList = await todoService
                    .CreateTodoListAsync(
                    token, 
                    $"OneClass : {classroom.Title}", 
                    cancellationToken
                );
                user.JoinClass(classroom.Id, "Student");
                user.AddTodoList(classroom.Id, todoList.Id);
                session.Store(user);

                await session.SaveChangesAsync(cancellationToken);

                return Results.Ok(user);
            }
        );
    }
}

using Marten;
using Carter;
using OneClass.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IDriveService, OneDriveService>();
builder.Services.AddSingleton<IAccessTokenService, AccessTokenService>();
builder.Services.AddSingleton<IStorage, AzureBlobStorage>();
builder.Services.AddSingleton<NewResourceNotificationService, NewResourceNotificationService>();
builder.Services.AddSingleton<IEmailService, OutlookEmailService>();
builder.Services.AddSingleton<ITodoService, TodoService>();

builder.Services.AddMarten(config =>
{
	var host = builder.Configuration.GetValue<string>("Postgres:Host");
	var port = builder.Configuration.GetValue<string>("Postgres:Port");
	var database = builder.Configuration.GetValue<string>("Postgres:Database");
	var username = builder.Configuration.GetValue<string>("Postgres:Username");
	var password = builder.Configuration.GetValue<string>("Postgres:Password");
	config.Connection($"Host={host};Port={port};Database={database};Username={username};Password={password}");
});

builder.Services.AddCors();
builder.Services.AddCarter();

var app = builder.Build();

app.UseCors(b =>
{
	b.AllowAnyOrigin();
	b.AllowAnyMethod();
	b.AllowAnyHeader();
	b.SetIsOriginAllowed((host) => true);
});
app.UseHttpsRedirection();

app.MapGet("/",
() => Results.Ok("Working")
);

app.MapCarter();

app.Run();
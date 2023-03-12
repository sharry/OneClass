using Marten;
using Carter;
using OneClass.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IAccessTokenService, AccessTokenService>();

builder.Services.AddMarten(config =>
{
	var host = builder.Configuration.GetValue<string>("Postgres:Host");
	var port = builder.Configuration.GetValue<string>("Postgres:Port");
	var database = builder.Configuration.GetValue<string>("Postgres:Database");
	var username = builder.Configuration.GetValue<string>("Postgres:Username");
	var password = builder.Configuration.GetValue<string>("Postgres:Password");
	config.Connection($"Host={host};Port={port};Database={database};Username={username};Password={password}");
});

builder.Services.AddCarter();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/",
() => Results.Ok("Working")
);

app.MapCarter();

app.Run();
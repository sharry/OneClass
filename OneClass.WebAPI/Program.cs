using System.Net.Http.Headers;
using Marten;
using Carter;
using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;

var builder = WebApplication.CreateBuilder(args);

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
app.MapGet("/users", (IDocumentSession session) =>
{
	var users = session.Query<UserData>();
	return Results.Ok(users);
});
app.MapGet("/my-data",
	async (
	IDocumentSession session,
	HttpContext context,
	CancellationToken cancellationToken) =>
	{
		var token = context.Request.Headers.Authorization[0];
		if (token is null)
		{
			return Results.Unauthorized();
		}
		using var httpClient = new HttpClient();
		httpClient.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Bearer", token.Replace("Bearer", ""));
		var response = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/me", cancellationToken);
		if (!response.IsSuccessStatusCode)
		{
			return Results.BadRequest();
		}
		var me = await response.Content.ReadFromJsonAsync<Me>(cancellationToken: cancellationToken);
		if (me is null)
		{
			return Results.BadRequest();
		}
		var existingUser = await session
			.Query<UserData>()
			.FirstOrDefaultAsync(x => x.Id == me.Id, cancellationToken);
		if (existingUser is not null)
		{
			return Results.Ok(existingUser); 
		}
		var userData = UserData.FromMe(me);
		session.Store(userData);
		await session.SaveChangesAsync(cancellationToken);
		return Results.Ok(userData);
});

app.MapCarter();

app.Run();
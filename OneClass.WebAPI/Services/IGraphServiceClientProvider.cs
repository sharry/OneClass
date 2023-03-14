using Microsoft.Graph;

namespace OneClass.WebAPI.Services;

public interface IGraphServiceClientProvider
{
	GraphServiceClient GetGraphServiceClient(string accessToken);
}
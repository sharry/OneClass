namespace OneClass.WebAPI.Services;

public interface IStorage
{
	Task UploadAsync(Stream stream, string path, CancellationToken cancellationToken = default);
	Task DeleteAsync(string path, CancellationToken cancellationToken = default);
}
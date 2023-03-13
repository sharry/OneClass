using Azure.Storage.Blobs;

namespace OneClass.WebAPI.Services;

public class AzureBlobStorage : IStorage
{
	private const string StorageConnectionString
		= "BlobEndpoint=https://oneclass.blob.core.windows.net/;QueueEndpoint=https://oneclass.queue.core.windows.net/;FileEndpoint=https://oneclass.file.core.windows.net/;TableEndpoint=https://oneclass.table.core.windows.net/;SharedAccessSignature=sv=2021-12-02&ss=bf&srt=co&sp=rwdlaciytfx&se=2023-03-20T03:28:16Z&st=2023-03-13T18:28:16Z&spr=https,http&sig=GONFxlI0hcmjzXDyl1pLA6aAeW%2FEJvnf0MyI9SP11Jg%3D";
	public async Task UploadAsync(Stream stream, string path, CancellationToken cancellationToken = default)
	{
		var blobClient = 
			new BlobClient(StorageConnectionString, "attachments", path);
		await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
		await blobClient.UploadAsync(stream, cancellationToken);
	}
	public async Task DeleteAsync(string path, CancellationToken cancellationToken = default)
	{
		var blobClient = 
			new BlobClient(StorageConnectionString, "attachments", path);
		await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
	}
}
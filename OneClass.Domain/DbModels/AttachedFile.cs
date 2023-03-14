namespace OneClass.Domain.DbModels;

public sealed record AttachedFile(
	string Id,
	string MimeType,
	string FileName,
	string Url,
	long Size
);
namespace OneClass.Domain.DbModels;

public sealed record AttachedFile(
	string MimeType,
	string FileName,
	string Url,
	string Size);
namespace OneClass.WebAPI.Assignment;

public sealed record EmailMessage(
	string Subject,
	EmailMessageBody Body,
	EmailMessageToRecipient[] ToRecipients
);

public sealed record EmailMessageBody(
	string Content,
	string ContentType
);

public sealed record EmailMessageToRecipient(
	EmailMessageToRecipientEmailAddress EmailAddress
);

public sealed record EmailMessageToRecipientEmailAddress(
	string Address
);
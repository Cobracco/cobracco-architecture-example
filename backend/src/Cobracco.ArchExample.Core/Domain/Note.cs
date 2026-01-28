namespace Cobracco.ArchExample.Core.Domain;

public sealed record Note(Guid Id, string Title, string Content, DateTime CreatedAt)
{
    public static Note Create(string title, string content, IClock clock)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        var now = clock.UtcNow;
        return new Note(Guid.NewGuid(), title.Trim(), content?.Trim() ?? string.Empty, now);
    }
}
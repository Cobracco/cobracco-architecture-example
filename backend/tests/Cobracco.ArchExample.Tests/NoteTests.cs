using Cobracco.ArchExample.Core.Domain;
using Xunit;

namespace Cobracco.ArchExample.Tests;

public sealed class NoteTests
{
    [Fact]
    public void Create_Throws_When_Title_Is_Empty()
    {
        var clock = new FakeClock(new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc));

        Assert.Throws<ArgumentException>(() => Note.Create(" ", "content", clock));
    }

    [Fact]
    public void Create_Sets_CreatedAt_From_Clock()
    {
        var expected = new DateTime(2025, 2, 2, 12, 30, 0, DateTimeKind.Utc);
        var clock = new FakeClock(expected);

        var note = Note.Create("Title", "Content", clock);

        Assert.Equal(expected, note.CreatedAt);
    }

    private sealed class FakeClock : IClock
    {
        public FakeClock(DateTime utcNow)
        {
            UtcNow = utcNow;
        }

        public DateTime UtcNow { get; }
    }
}
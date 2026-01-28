using Cobracco.ArchExample.Core.Domain;

namespace Cobracco.ArchExample.Infrastructure.Time;

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
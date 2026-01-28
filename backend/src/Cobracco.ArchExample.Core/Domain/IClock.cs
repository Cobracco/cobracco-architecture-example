namespace Cobracco.ArchExample.Core.Domain;

public interface IClock
{
    DateTime UtcNow { get; }
}
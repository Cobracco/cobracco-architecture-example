using Cobracco.ArchExample.Core.Domain;

namespace Cobracco.ArchExample.Core.Application;

public interface INoteRepository
{
    Task<IReadOnlyList<Note>> ListAsync(CancellationToken cancellationToken = default);
    Task<Note?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Note note, CancellationToken cancellationToken = default);
}
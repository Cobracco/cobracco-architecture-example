using Cobracco.ArchExample.Core.Domain;

namespace Cobracco.ArchExample.Core.Application;

public sealed class ListNotes
{
    private readonly INoteRepository _repository;

    public ListNotes(INoteRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Note>> ExecuteAsync(CancellationToken cancellationToken = default)
        => _repository.ListAsync(cancellationToken);
}
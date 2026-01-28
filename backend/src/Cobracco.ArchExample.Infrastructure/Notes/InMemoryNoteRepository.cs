using Cobracco.ArchExample.Core.Application;
using Cobracco.ArchExample.Core.Domain;

namespace Cobracco.ArchExample.Infrastructure.Notes;

public sealed class InMemoryNoteRepository : INoteRepository
{
    private readonly List<Note> _notes = new();
    private readonly object _lock = new();

    public Task<IReadOnlyList<Note>> ListAsync(CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var ordered = _notes
                .OrderByDescending(note => note.CreatedAt)
                .ToList()
                .AsReadOnly();
            return Task.FromResult<IReadOnlyList<Note>>(ordered);
        }
    }

    public Task<Note?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            return Task.FromResult(note);
        }
    }

    public Task AddAsync(Note note, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            _notes.Add(note);
        }

        return Task.CompletedTask;
    }
}
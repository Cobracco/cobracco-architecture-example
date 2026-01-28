using Cobracco.ArchExample.Core.Domain;

namespace Cobracco.ArchExample.Core.Application;

public sealed class CreateNote
{
    private readonly INoteRepository _repository;
    private readonly IClock _clock;

    public CreateNote(INoteRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

    public async Task<Note> ExecuteAsync(string title, string content, CancellationToken cancellationToken = default)
    {
        var note = Note.Create(title, content, _clock);
        await _repository.AddAsync(note, cancellationToken);
        return note;
    }
}
using Cobracco.ArchExample.Core.Application;
using Cobracco.ArchExample.Core.Domain;
using Cobracco.ArchExample.Infrastructure.Notes;
using Cobracco.ArchExample.Infrastructure.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<INoteRepository, InMemoryNoteRepository>();
builder.Services.AddSingleton<IClock, SystemClock>();
builder.Services.AddScoped<CreateNote>();
builder.Services.AddScoped<ListNotes>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/api/notes", async (ListNotes listNotes, CancellationToken cancellationToken) =>
{
    var notes = await listNotes.ExecuteAsync(cancellationToken);
    var response = notes.Select(NoteResponse.From);
    return Results.Ok(response);
});

app.MapPost("/api/notes", async (CreateNote createNote, CreateNoteRequest request, CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(request.Title))
    {
        return Results.BadRequest(new { error = "Title is required." });
    }

    var note = await createNote.ExecuteAsync(request.Title, request.Content ?? string.Empty, cancellationToken);
    return Results.Created($"/api/notes/{note.Id}", NoteResponse.From(note));
});

app.Run();

public sealed record CreateNoteRequest(string Title, string? Content);

public sealed record NoteResponse(Guid Id, string Title, string Content, DateTime CreatedAt)
{
    public static NoteResponse From(Note note)
        => new(note.Id, note.Title, note.Content, note.CreatedAt);
}
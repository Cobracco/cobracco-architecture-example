import { useEffect, useState } from 'react'
import './App.css'

type Note = {
  id: string
  title: string
  content: string
  createdAt: string
}

export default function App() {
  const [notes, setNotes] = useState<Note[]>([])
  const [title, setTitle] = useState('')
  const [content, setContent] = useState('')
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const loadNotes = async () => {
    setLoading(true)
    setError(null)
    try {
      const response = await fetch('/api/notes')
      if (!response.ok) {
        throw new Error('Failed to load notes.')
      }
      const data: Note[] = await response.json()
      setNotes(data)
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unexpected error.')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    void loadNotes()
  }, [])

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault()
    if (!title.trim()) {
      setError('Title is required.')
      return
    }

    setLoading(true)
    setError(null)
    try {
      const response = await fetch('/api/notes', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title, content })
      })

      if (!response.ok) {
        throw new Error('Failed to create note.')
      }

      setTitle('')
      setContent('')
      await loadNotes()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unexpected error.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="page">
      <header className="header">
        <div>
          <p className="eyebrow">Cobracco</p>
          <h1>Notes Architecture Example</h1>
          <p className="subtitle">Minimal .NET 9 + React example with clean layering.</p>
        </div>
      </header>

      <main className="content">
        <section className="panel">
          <h2>Create note</h2>
          <form onSubmit={handleSubmit} className="form">
            <label>
              Title
              <input
                value={title}
                onChange={(event) => setTitle(event.target.value)}
                placeholder="A short title"
              />
            </label>
            <label>
              Content
              <textarea
                value={content}
                onChange={(event) => setContent(event.target.value)}
                placeholder="Optional details"
              />
            </label>
            <button type="submit" disabled={loading}>Create</button>
          </form>
          {error && <p className="error">{error}</p>}
        </section>

        <section className="panel">
          <div className="panel-header">
            <h2>Notes</h2>
            <button onClick={() => void loadNotes()} disabled={loading}>Refresh</button>
          </div>
          {loading && notes.length === 0 ? (
            <p className="muted">Loading notes...</p>
          ) : notes.length === 0 ? (
            <p className="muted">No notes yet.</p>
          ) : (
            <ul className="notes">
              {notes.map((note) => (
                <li key={note.id} className="note">
                  <div>
                    <h3>{note.title}</h3>
                    <p>{note.content || '—'}</p>
                  </div>
                  <span>{new Date(note.createdAt).toLocaleString()}</span>
                </li>
              ))}
            </ul>
          )}
        </section>
      </main>
    </div>
  )
}
using MongoDB.Driver;
using simple_api.src.API.Models;

namespace simple_api.src.Services
{
    public class NoteService
    {
        private readonly IMongoCollection<Note> _notesCollection;

        public NoteService(IMongoCollection<Note> notesCollection)
        {
            _notesCollection = notesCollection ?? throw new ArgumentNullException(nameof(notesCollection));
        }

        public async Task<List<Note>> GetNotesAsync(){
            return await _notesCollection.Find(note => true).ToListAsync();
        }

        public virtual async Task<Note> GetNoteByIdAsync(string id){
            var note = await _notesCollection.Find<Note>(note => note.Id == id).FirstOrDefaultAsync();
            return note;
        }

        public async Task AddNoteAsync(Note note){
            await _notesCollection.InsertOneAsync(note);
        }

        public async Task UpdateNoteAsync(string id, Note updatedNote)
        {
            await _notesCollection.ReplaceOneAsync(note => note.Id == id, updatedNote);
        }
     }
}
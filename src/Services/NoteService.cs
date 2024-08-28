using MongoDB.Driver;
using simple_api.src.API.Models;

namespace simple_api.src.Services
{
    public class NoteService
    {
        private readonly IMongoCollection<Note> _notesCollection;

        public NoteService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("notes-db");
            _notesCollection = database.GetCollection<Note>("note");
        }

        public async Task<List<Note>> GetNotesAsync(){
            return await _notesCollection.Find(note => true).ToListAsync();
        }

        public void AddNote(Note note){
            _notesCollection.InsertOne(note);
        }
     }
}
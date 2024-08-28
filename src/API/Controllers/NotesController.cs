using simple_api.src.Services;
using simple_api.src.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace simple_api.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var notes = await _noteService.GetNotesAsync();
            return Ok(notes);
        }

        //GET: api/Notes{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(string id){
            var note = await _noteService.GetNoteById(id);
            if(note == null){
                return NotFound();
            }
            return Ok(note);
        }

        //POST: api/Notes
        [HttpPost]
        public IActionResult Create(Note note){
            if(note == null){
                return BadRequest("Note cannot be null");
            }

            _noteService.AddNote(note);
            return CreatedAtAction(nameof(Get), new {id = note.Id}, note);
        }
    }
}
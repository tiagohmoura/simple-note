using simple_api.src.Services;
using simple_api.src.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var notes = await _noteService.GetNotesAsync();
            return Ok(notes);
        }

        //GET: api/Notes{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteByIdAsync(string id){
            var note = await _noteService.GetNoteByIdAsync(id);
            if(note == null){
                return NotFound();
            }
            return Ok(note);
        }

        //POST: api/Notes
        [Authorize]
        [HttpPost]
        public IActionResult Create(Note note){
            if(note == null){
                return BadRequest("Note cannot be null");
            }

            _noteService.AddNoteAsync(note);
            return CreatedAtAction(nameof(Get), new {id = note.Id}, note);
        }

        //PUT: api/Notes/{id}
        [Authorize]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpadateNote(string id, [FromBody] Note updatedNote){
            var existingNote = await _noteService.GetNoteByIdAsync(id);

            if(existingNote == null){
                return NotFound(new { message = $"Note with id '{id}' not found." });
            }

            updatedNote.Id = existingNote.Id;

            await _noteService.UpdateNoteAsync(id, updatedNote);

            return NoContent();
        }
    }
}
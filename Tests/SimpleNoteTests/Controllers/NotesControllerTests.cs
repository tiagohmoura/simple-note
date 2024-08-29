using Microsoft.AspNetCore.Mvc;
using Moq;
using simple_api.src.API.Controllers;
using simple_api.src.API.Models;
using simple_api.src.Services;
using Xunit;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SimpleNoteTests.Controllers
{
    public class NotesControllerTests
    {
        private readonly NotesController _controller;
        private readonly Mock<NoteService> _mockNoteService;

        public NotesControllerTests()
        {
            var mockCollection = new Mock<IMongoCollection<Note>>();
            _mockNoteService = new Mock<NoteService>(mockCollection.Object);
            _controller = new NotesController(_mockNoteService.Object);
        }
        
        [Fact]
        public async void GetNoteById_ReturnsOkResult()
        {
            // Arrange
            var noteId = "testId";
            var note = new Note { Id = noteId, Title = "Test Note" };
            _mockNoteService.Setup(service => service.GetNoteByIdAsync(noteId)).ReturnsAsync(note);

            // Act
            var result = await _controller.GetNoteByIdAsync(noteId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedNote = Assert.IsType<Note>(actionResult.Value);
            Assert.Equal(noteId, returnedNote.Id);
        }
    }
}
using Moq;
using Xunit;
using simple_api.src.Services;
using simple_api.src.API.Models;
using MongoDB.Driver;

namespace SimpleNoteTests.Services
{
    public class NoteServiceTests
    {
        private readonly NoteService _noteService;
        private readonly Mock<IMongoCollection<Note>> _mockCollection;
        private readonly Mock<IAsyncCursor<Note>> _mockCursor;

        public NoteServiceTests()
        {
            _mockCollection = new Mock<IMongoCollection<Note>>();
            _mockCursor = new Mock<IAsyncCursor<Note>>();
            
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);
            
            _mockCursor.Setup(x => x.Current).Returns(new List<Note> { new Note { Id = "testId", Title = "Test" } });
            _mockCollection.Setup(x => x.FindSync(It.IsAny<FilterDefinition<Note>>(), It.IsAny<FindOptions<Note, Note>>(), It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _noteService = new NoteService(_mockCollection.Object);
        }

        [Fact]
        public async Task AddNote_ShouldInsertNote()
        {
            // Arrange
            var newNote = new Note { Title = "Test Note", Content = "Test Content" };

            // Act
            await _noteService.AddNoteAsync(newNote);

            // Assert
            _mockCollection.Verify(c => c.InsertOneAsync(newNote, null, default), Times.Once);
        }
    }
}

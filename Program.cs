using simple_api.src.API.Routes; 
using simple_api.src.Services;
using simple_api.src.API.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MongoDB
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = builder.Configuration.GetConnectionString("MongoDB");
    return new MongoClient(settings);
});

builder.Services.AddScoped<IMongoCollection<Note>>(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    var database = client.GetDatabase("notes-db");
    return database.GetCollection<Note>("note");
});

builder.Services.AddScoped<NoteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Rotas
//app.AddRotasNotes();
app.MapControllers();

app.Run();


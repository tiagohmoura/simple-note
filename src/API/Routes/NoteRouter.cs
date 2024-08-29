namespace simple_api.src.API.Routes
{
    public static class NoteRouters
    {
    public static void AddRotasNotes(this WebApplication app)
        {
            //var rotasNotes:RouteGroupBuilder = app.MapGroup(prefix:"notes");
            //rotasNotes.MapGet("", () => "Hello Notes");
            app.MapGet("notes", () => "Hello Notes");
        }
    }
}
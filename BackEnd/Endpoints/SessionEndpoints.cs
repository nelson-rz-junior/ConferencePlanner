using BackEnd.Data.Context;
using BackEnd.Data.Models;
using BackEnd.Infrastructure;
using ConferencePlanner.DTO;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints;

public static class SessionEndpoints
{
    public static void MapSessionEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Session/", async (ConferencePlannerContext db) =>
        {
            var sessionResponse = await db.Sessions.AsNoTracking()
                .Include(s => s.Track)
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Speaker)
                .Select(m => m.MapSessionResponse())
                .ToListAsync();

            return sessionResponse is List<SessionResponse> model
                ? Results.Ok(model)
                : Results.NotFound();
        })
        .WithTags("Session")
        .WithName("GetAllSessions")
        .Produces<List<SessionResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapGet("/api/Session/{id}", async (int id, ConferencePlannerContext db) =>
        {
            var session = await db.Sessions.AsNoTracking()
                .Include(s => s.Track)
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Speaker)
                .SingleOrDefaultAsync(s => s.Id == id);

            return session is Data.Models.Session model
                ? Results.Ok(model.MapSessionResponse())
                : Results.NotFound();

        })
        .WithTags("Session")
        .WithName("Getsession")
        .Produces<SessionResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPost("/api/Session/", async (ConferencePlanner.DTO.Session input, ConferencePlannerContext db) =>
        {
            var session = new Data.Models.Session
            {
                Title = input.Title,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                Abstract = input.Abstract,
                TrackId = input.TrackId
            };

            db.Sessions.Add(session);
            await db.SaveChangesAsync();

            return Results.Created($"/api/Session/{session.Id}", session.MapSessionResponse());
        })
        .WithTags("Session")
        .WithName("CreateSession")
        .Produces<SessionResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict);

        routes.MapPut("/api/Session/{id}", async (int id, ConferencePlanner.DTO.Session input, ConferencePlannerContext db) =>
        {
            var session = await db.Sessions.FindAsync(id);

            if (session is null)
            {
                return Results.NotFound();
            }

            session.Id = input.Id;
            session.Title = input.Title;
            session.Abstract = input.Abstract;
            session.StartTime = input.StartTime;
            session.EndTime = input.EndTime;
            session.TrackId = input.TrackId;

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithTags("Session")
        .WithName("UpdateSession")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapDelete("/api/Sessions/{id}/", async (int id, ConferencePlannerContext db) =>
        {
            if (await db.Sessions.FindAsync(id) is Data.Models.Session session)
            {
                db.Sessions.Remove(session);
                await db.SaveChangesAsync();

                return Results.Ok();
            }

            return Results.NotFound();
        })
        .WithTags("Session")
        .WithName("DeleteSession")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPost("api/Sessions/Upload", async (HttpRequest req, ConferencePlannerContext db) =>
        {
            if (db.Sessions.Any())
            {
                return Results.Conflict("Sessions already uploaded");
            }

            var loader = new TechoramaDataLoader();
            await loader.LoadDataAsync(req.Body, db);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithTags("Session")
        .WithName("UploadSession")
        .Accepts<IFormFile>("text/plain");
    }
}

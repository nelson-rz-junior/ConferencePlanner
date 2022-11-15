using Microsoft.EntityFrameworkCore;
using BackEnd.Data.Context;
using BackEnd.Data.Models;
using BackEnd.Infrastructure;
using dtos = ConferencePlanner.DTO;

namespace BackEnd.Endpoints;

public static class SpeakerEndpoints
{
    public static void MapSpeakerEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/speakers", async (ConferencePlannerContext db) =>
        {
            var speakers = await db.Speakers.AsNoTracking()
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Session)
                .Select(s => s.MapSpeakerResponse())
                .ToListAsync();

            return speakers;
        })
        .WithTags("Speaker")
        .WithName("GetAllSpeakers")
        .Produces<List<dtos.Speaker>>(StatusCodes.Status200OK);

        routes.MapGet("/api/speakers/{id}", async (int id, ConferencePlannerContext db) =>
        {
            var speaker = await db.Speakers.AsNoTracking()
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Session)
                .SingleOrDefaultAsync(s => s.Id == id);

            return speaker is Speaker model
                ? Results.Ok(model.MapSpeakerResponse())
                : Results.NotFound();
        })
        .WithTags("Speaker")
        .WithName("GetSpeakerById")
        .Produces<dtos.Speaker>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}

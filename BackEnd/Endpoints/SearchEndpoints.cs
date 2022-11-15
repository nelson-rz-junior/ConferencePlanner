using BackEnd.Data.Context;
using BackEnd.Infrastructure;
using ConferencePlanner.DTO;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints;

public static class SearchEndpoints
{
    public static void MapSearchEndpoints(this IEndpointRouteBuilder router)
    {
        router.MapGet("/api/search/{term}", async (string term, ConferencePlannerContext db) =>
        {
            term = term.ToLower();

            var sessionResults = await db.Sessions.Include(s => s.Track)
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Speaker)
                .Where(s => s.Title!.ToLower().Contains(term) || s.Track!.Name!.ToLower().Contains(term))
                .ToListAsync();

            var speakerResults = await db.Speakers.Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Session)
                .Where(s => s.Name!.ToLower().Contains(term) || s.Bio!.ToLower().Contains(term) || s.WebSite!.ToLower().Contains(term))
                .ToListAsync();

            var results = sessionResults.Select(s => new SearchResult
            {
                Type = SearchResultType.Session,
                Session = s.MapSessionResponse()
            })
            .Concat(speakerResults.Select(s => new SearchResult
            {
                Type = SearchResultType.Speaker,
                Speaker = s.MapSpeakerResponse()
            }));

            return results is IEnumerable<SearchResult> model
                ? Results.Ok(results)
                : Results.NotFound();
        })
        .WithTags("Search")
        .WithName("GetSearchResults")
        .Produces<IEnumerable<SearchResult>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}

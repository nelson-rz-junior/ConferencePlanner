using BackEnd.Data.Context;
using BackEnd.Data.Models;
using BackEnd.Infrastructure;
using ConferencePlanner.DTO;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints;

public static class AttendeeEndpoints
{
    public static void MapAttendeeEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Attendee/{username}", async (string username, ConferencePlannerContext db) =>
        {
            var attendee = await db.Attendees.Include(a => a.SessionAttendees)
                .ThenInclude(sa => sa.Session)
                .SingleOrDefaultAsync(a => a.UserName == username);

            return attendee is Data.Models.Attendee model
                ? Results.Ok(model.MapAttendeeResponse())
                : Results.NotFound();
        })
        .WithTags("Attendee")
        .WithName("GetAttendee")
        .Produces<AttendeeResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPost("/api/Attendee/", async (ConferencePlanner.DTO.Attendee input, ConferencePlannerContext db) =>
        {
            // Check if the attendee already exists
            var existingAttendee = await db.Attendees
                .Where(a => a.UserName == input.UserName)
                .FirstOrDefaultAsync();

            if (existingAttendee == null)
            {
                var attendee = new Data.Models.Attendee
                {
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    UserName = input.UserName,
                    EmailAddress = input.EmailAddress
                };

                db.Attendees.Add(attendee);
                await db.SaveChangesAsync();

                var result = attendee.MapAttendeeResponse();

                return Results.Created($"/api/Attendee/{attendee.UserName}", result);
            }
            else
            {
                return Results.Conflict();
            }
        })
        .WithTags("Attendee")
        .WithName("CreateAttendee")
        .Produces<AttendeeResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict);

        routes.MapPost("/api/Attendee/{username}/Session/{sessionId}", async (string username, int sessionId, ConferencePlannerContext db) =>
        {
            var attendee = await db.Attendees.Include(a => a.SessionAttendees)
                .ThenInclude(sa => sa.Session)
                .SingleOrDefaultAsync(a => a.UserName == username);

            if (attendee == null)
            {
                return Results.NotFound(new { Attendee = username });
            }

            var session = await db.Sessions.FindAsync(sessionId);

            if (session == null)
            {
                return Results.NotFound(new { Session = sessionId });
            }

            attendee.SessionAttendees.Add(new SessionAttendee
            {
                AttendeeId = attendee.Id,
                SessionId = sessionId
            });

            await db.SaveChangesAsync();

            var result = attendee.MapAttendeeResponse();

            return Results.Created($"/api/Attendee/{result.UserName}", result);
        })
        .WithTags("Attendee")
        .WithName("AddAttendeeSession")
        .Produces<AttendeeResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapDelete("/api/Attendee/{username}/Session/{sessionId}", async (string username, int sessionId, ConferencePlannerContext db) =>
        {
            var attendee = await db.Attendees.Include(a => a.SessionAttendees)
                .SingleOrDefaultAsync(a => a.UserName == username);

            if (attendee is Data.Models.Attendee)
            {
                var session = await db.Sessions.FindAsync(sessionId);

                if (session is Data.Models.Session)
                {
                    var sessionAttendee = attendee.SessionAttendees
                        .FirstOrDefault(sa => sa.SessionId == sessionId);

                    if (sessionAttendee is SessionAttendee)
                    {
                        attendee.SessionAttendees.Remove(sessionAttendee);
                    }

                    await db.SaveChangesAsync();

                    return Results.Ok();
                }
            }

            return Results.NotFound();
        })
        .WithTags("Attendee")
        .WithName("RemoveSessionFromAttendee")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}

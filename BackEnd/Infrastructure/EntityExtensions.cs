using BackEnd.Data.Models;
using dtos = ConferencePlanner.DTO;

namespace BackEnd.Infrastructure;

public static class EntityExtensions
{
    public static dtos.SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
        new()
        {
            Id = speaker.Id,
            Name = speaker.Name,
            Bio = speaker.Bio,
            WebSite = speaker.WebSite,
            Sessions = speaker.SessionSpeakers?.Select(ss => new dtos.Session
            {
                Id = ss.SessionId,
                Title = ss.Session.Title
            })
            .ToList() ?? new()
        };

    public static dtos.SessionResponse MapSessionResponse(this Session session) =>
        new()
        {
            Id = session.Id,
            Title = session.Title,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Speakers = session.SessionSpeakers?.Select(ss => new dtos.Speaker
            {
                Id = ss.SpeakerId,
                Name = ss.Speaker.Name
            })
            .ToList() ?? new(),
            TrackId = session.TrackId,
            Track = new dtos.Track
            {
                Id = session?.TrackId ?? 0,
                Name = session?.Track?.Name
            },
            Abstract = session?.Abstract
        };

    public static dtos.AttendeeResponse MapAttendeeResponse(this Attendee attendee) =>
        new()
        {
            Id = attendee.Id,
            FirstName = attendee.FirstName,
            LastName = attendee.LastName,
            UserName = attendee.UserName,
            Sessions = attendee.SessionAttendees?.Select(sa => new dtos.Session
            {
                Id = sa.SessionId,
                Title = sa.Session.Title,
                StartTime = sa.Session.StartTime,
                EndTime = sa.Session.EndTime
            })
            .ToList() ?? new()
        };
}

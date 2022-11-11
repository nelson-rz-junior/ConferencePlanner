using BackEnd.Data.Models;

namespace BackEnd.Infrastructure;

public static class EntityExtensions
{
    public static ConferencePlanner.DTO.SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
        new()
        {
            Id = speaker.Id,
            Name = speaker.Name,
            Bio = speaker.Bio,
            WebSite = speaker.WebSite,
            Sessions = speaker.SessionSpeakers?.Select(ss => new ConferencePlanner.DTO.Session
            {
                Id = ss.SessionId,
                Title = ss.Session.Title
            })
            .ToList() ?? new()
        };

    public static ConferencePlanner.DTO.SessionResponse MapSessionResponse(this Session session) =>
        new()
        {
            Id = session.Id,
            Title = session.Title,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Speakers = session.SessionSpeakers?.Select(ss => new ConferencePlanner.DTO.Speaker
            {
                Id = ss.SpeakerId,
                Name = ss.Speaker.Name
            })
            .ToList() ?? new(),
            TrackId = session.TrackId,
            Track = new ConferencePlanner.DTO.Track
            {
                Id = session?.TrackId ?? 0,
                Name = session?.Track?.Name
            },
            Abstract = session?.Abstract
        };

    public static ConferencePlanner.DTO.AttendeeResponse MapAttendeeResponse(this Attendee attendee) =>
        new()
        {
            Id = attendee.Id,
            FirstName = attendee.FirstName,
            LastName = attendee.LastName,
            UserName = attendee.UserName,
            Sessions = attendee.SessionAttendees?.Select(sa => new ConferencePlanner.DTO.Session
            {
                Id = sa.SessionId,
                Title = sa.Session.Title,
                StartTime = sa.Session.StartTime,
                EndTime = sa.Session.EndTime
            })
            .ToList() ?? new()
        };
}

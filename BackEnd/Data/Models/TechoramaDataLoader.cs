using BackEnd.Data.Context;
using System.Text.Json;

namespace BackEnd.Data.Models;

public class TechoramaDataLoader : DataLoader
{
    public async override Task LoadDataAsync(Stream fileStream, ConferencePlannerContext db)
    {
        var addedSpeakers = new Dictionary<string, Speaker>();
        var addedTracks = new Dictionary<string, Track>();

        var reader = new StreamReader(fileStream);
        var text = await reader.ReadToEndAsync();

        var techoramaSessions = JsonSerializer.Deserialize<List<TechoramaSession>>(text) ?? new();

        foreach (TechoramaSession item in techoramaSessions)
        {
            //These are all required to add to the schedule
            var speakers = item.Speakers?.Split(',');
            if (speakers is null || item.TimeSlot is null || item.Date is null || item.Track is null)
            {
                continue;
            }

            foreach (var thisSpeakerName in speakers)
            {
                if (!addedSpeakers.ContainsKey(thisSpeakerName))
                {
                    var thisSpeaker = new Speaker { Name = thisSpeakerName };
                    db.Speakers.Add(thisSpeaker);
                    addedSpeakers.Add(thisSpeakerName, thisSpeaker);
                }

                if (!addedTracks.ContainsKey(item.Track))
                {
                    var thisTrack = new Track { Name = item.Track };
                    db.Tracks.Add(thisTrack);
                    addedTracks.Add(item.Track, thisTrack);
                }
            }

            //"08:45 - 09:45"
            string[] timeSlotParts = item.TimeSlot.Split(" - ");

            //"24 May 2022 | 08:45 - 09:45"
            string date = item.Date.Split(" | ")[0];

            var session = new Session
            {
                Title = item.Title,
                StartTime = DateTime.Parse($"{date} {timeSlotParts[0]}"),
                EndTime = DateTime.Parse($"{date} {timeSlotParts[1]}"),
                Track = addedTracks[item.Track],
                Abstract = item.Description,
                SessionSpeakers = new List<SessionSpeaker>()
            };

            foreach (var sp in speakers)
            {
                session.SessionSpeakers.Add(new SessionSpeaker
                {
                    Session = session,
                    Speaker = addedSpeakers[sp]
                });
            }

            db.Sessions.Add(session);
        }
    }
}

﻿namespace ConferencePlanner.DTO;

public class SpeakerResponse : Speaker
{
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}

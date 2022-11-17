import { Speaker } from "./Speaker";
import { Track } from "./Track";

export class Session {
  id: number;
  title: string;
  abstract: string;
  startTime: Date;
  endTime: Date;
  duration: string;
  trackId: number;
  track: Track;
  speakers: Speaker[]
}

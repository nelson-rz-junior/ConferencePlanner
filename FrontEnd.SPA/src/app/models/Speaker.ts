import { Session } from "./Session";

export class Speaker {
  id: number;
  name: string;
  bio?: string;
  webSite?: string;
  sessions?: Session[]
}

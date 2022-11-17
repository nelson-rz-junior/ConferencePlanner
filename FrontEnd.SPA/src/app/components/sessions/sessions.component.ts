import { Component, OnInit } from '@angular/core';
import { Session } from 'src/app/models/Session';
import { SessionsService } from 'src/app/services/sessions.service';

@Component({
  selector: 'app-sessions',
  templateUrl: './sessions.component.html',
  styleUrls: ['./sessions.component.css']
})
export class SessionsComponent implements OnInit {
  public sessions: Session[] = [];

  constructor(private sessionService: SessionsService) { }

  ngOnInit() {
    this.getSessions();
  }

  private getSessions(): void {
    this.sessionService.getSessions()
      .subscribe(
        (result: Session[]) => {
          this.sessions = result;
        },
        (error: any) => {
          console.error(error)
        }
      );
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Session } from 'src/app/models/Session';
import { SessionsService } from 'src/app/services/sessions.service';

@Component({
  selector: 'app-session-details',
  templateUrl: './session-details.component.html',
  styleUrls: ['./session-details.component.scss']
})
export class SessionDetailsComponent implements OnInit {
  public sessionId: number;
  public session: Session = {} as Session;

  constructor(private sessionService: SessionsService,
    private actRouter: ActivatedRoute) { }

  ngOnInit() {
    this.sessionId = +this.actRouter.snapshot.paramMap.get('id');
    this.getSession(this.sessionId);
  }

  public getSession(id: number): void {
    this.sessionService.getSession(id)
      .subscribe(
        (result: Session) => {
          this.session = result;
        },
        (error: any) => {
          console.error(error);
        }
      );
  }
}

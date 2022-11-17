import { Component, OnInit } from '@angular/core';
import { Speaker } from 'src/app/models/Speaker';
import { SpeakersService } from 'src/app/services/speakers.service';

@Component({
  selector: 'app-speakers',
  templateUrl: './speakers.component.html',
  styleUrls: ['./speakers.component.css']
})
export class SpeakersComponent implements OnInit {
  public speakers: Speaker[] = [];

  constructor(private speakerService: SpeakersService) { }

  ngOnInit() {
    this.getSpeakers();
  }

  private getSpeakers(): void {
    this.speakerService.getSpeakers()
      .subscribe(
        (result: Speaker[]) => {
          this.speakers = result;
        },
        (error: any) => {
          console.error(error);
        });
  }
}

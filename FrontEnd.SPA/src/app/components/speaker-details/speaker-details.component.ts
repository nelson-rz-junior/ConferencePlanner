import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Speaker } from 'src/app/models/Speaker';
import { SpeakersService } from 'src/app/services/speakers.service';

@Component({
  selector: 'app-speaker-details',
  templateUrl: './speaker-details.component.html',
  styleUrls: ['./speaker-details.component.scss']
})
export class SpeakerDetailsComponent implements OnInit {
  public speakerId: number;
  public speaker: Speaker = {} as Speaker;

  constructor(private speakerService: SpeakersService,
    private actRouter: ActivatedRoute) { }

  ngOnInit() {
    this.speakerId = +this.actRouter.snapshot.paramMap.get('id');
    this.getSpeaker(this.speakerId);
  }

  public getSpeaker(id: number): void {
    this.speakerService.getSpeaker(id)
      .subscribe(
        (result: Speaker) => {
          this.speaker = result;
        },
        (error: any) => {
          console.log(error);
        }
      );
  }
}

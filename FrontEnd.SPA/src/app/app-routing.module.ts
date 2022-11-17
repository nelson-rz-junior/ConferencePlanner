import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { SessionDetailsComponent } from './components/session-details/session-details.component';
import { SessionsComponent } from './components/sessions/sessions.component';
import { SpeakerDetailsComponent } from './components/speaker-details/speaker-details.component';
import { SpeakersComponent } from './components/speakers/speakers.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'sessions', component: SessionsComponent },
  { path: 'sessions/:id', component: SessionDetailsComponent },
  { path: 'speakers', component: SpeakersComponent },
  { path: 'speakers/:id', component: SpeakerDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

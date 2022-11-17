import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AppComponent } from './components/root/app.component';
import { SessionsComponent } from './components/sessions/sessions.component';
import { SpeakersComponent } from './components/speakers/speakers.component';
import { SessionsService } from './services/sessions.service';
import { SpeakersService } from './services/speakers.service';
import { SessionDetailsComponent } from './components/session-details/session-details.component';
import { SpeakerDetailsComponent } from './components/speaker-details/speaker-details.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SessionsComponent,
    SessionDetailsComponent,
    SpeakersComponent,
    SpeakerDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    SessionsService,
    SpeakersService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

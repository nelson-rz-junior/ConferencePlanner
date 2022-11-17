import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { take } from 'rxjs/operators';
import { Speaker } from '../models/Speaker';

@Injectable()
export class SpeakersService {
  private apiURL: string = `${environment.API_URL}/api/speakers`;
  private headers: HttpHeaders = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  public getSpeakers(): Observable<Speaker[]> {
    return this.http
      .get<Speaker[]>(this.apiURL, { headers: this.headers })
      .pipe(take(1));
  }

  public getSpeaker(id: number): Observable<Speaker> {
    return this.http
      .get<Speaker>(`${this.apiURL}/${id}`, { headers: this.headers })
      .pipe(take(1));
  }
}

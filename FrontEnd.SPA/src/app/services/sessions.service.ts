import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Session } from '../models/Session';

@Injectable()
export class SessionsService {
  private apiURL: string = `${environment.API_URL}/api/sessions`;
  private headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  public getSessions(): Observable<Session[]> {
    return this.http
      .get<Session[]>(this.apiURL, { headers: this.headers })
      .pipe(take(1));
  }

  public getSession(id: number): Observable<Session> {
    return this.http
      .get<Session>(`${this.apiURL}/${id}`, { headers: this.headers })
      .pipe(take(1));
  }
}

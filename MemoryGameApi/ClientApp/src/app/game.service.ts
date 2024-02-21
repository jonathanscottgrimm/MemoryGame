import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from './models/game.model';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private apiUrl = 'https://localhost:7247/api/game';

  constructor(private http: HttpClient) {}

  startGame(): Observable<Game> {
    return this.http.get<Game>(`${this.apiUrl}/start`);
  }

  playTurn(cardPosition1: number, cardPosition2: number): Observable<Game> {
    return this.http.post<Game>(`${this.apiUrl}/turn`, {
      cardPosition1,
      cardPosition2,
    });
  }

  getGameState(): Observable<Game> {
    return this.http.get<Game>(`${this.apiUrl}/state`);
  }
}

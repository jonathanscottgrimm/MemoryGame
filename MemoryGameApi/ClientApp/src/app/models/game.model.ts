import { Card } from './card.model';

export interface Game {
  cards: Card[];
  player1Score: number;
  player2Score: number;
  currentPlayer: number;
  isGameOver: boolean;
}

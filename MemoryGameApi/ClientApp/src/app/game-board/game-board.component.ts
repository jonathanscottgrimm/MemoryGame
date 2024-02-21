import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { GameService } from '../game.service';
import { Game } from '../models/game.model';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css'],
})
export class GameBoardComponent implements OnInit {
  game!: Game;
  firstCardIndex: number | null = null;

  constructor(
    private gameService: GameService,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.startNewGame();
  }

  startNewGame(): void {
    this.gameService.startGame().subscribe((game) => {
      this.game = game;
      this.changeDetectorRef.detectChanges();
    });
  }

  onCardClick(index: number): void {
    if (
      !this.game ||
      this.game.cards[index].isMatched ||
      this.game.cards[index].isFlipped
    ) {
      return;
    }

    this.game.cards[index].isFlipped = true;

    if (this.firstCardIndex === null) {
      this.firstCardIndex = index;
    } else {
      this.checkForMatch(this.firstCardIndex, index);
      this.firstCardIndex = null;
    }
  }

  private checkForMatch(firstCardIndex: number, secondCardIndex: number): void {
    setTimeout(() => {
      const firstCard = this.game.cards[firstCardIndex];
      const secondCard = this.game.cards[secondCardIndex];

      if (firstCard.value !== secondCard.value) {
        this.game.cards[firstCardIndex].isFlipped = false;
        this.game.cards[secondCardIndex].isFlipped = false;

        this.game.currentPlayer = this.game.currentPlayer === 1 ? 2 : 1;
      } else {
        if (this.game.currentPlayer === 1) {
          this.game.player1Score += 1;
        } else {
          this.game.player2Score += 1;
        }
        this.game.cards[firstCardIndex].isMatched = true;
        this.game.cards[secondCardIndex].isMatched = true;

        // This needs improving. im using it to trigger change detection but it's messing up the order of the cards.
        // there needs to be dummy values here when removed or some ui kinda hack
        this.game.cards = [...this.game.cards];
      }
      this.changeDetectorRef.detectChanges();
    }, 1000);
  }
}

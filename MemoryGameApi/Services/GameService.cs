using MemoryGameApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGameApi.Services
{
    public class GameService
    {
        private Game _currentGame;

        public Game StartNewGame()
        {
            var rnd = new Random();
            var cardValues = Enumerable.Range(1, 10).SelectMany(x => new string[] { x.ToString(), x.ToString() }).ToList();
            var shuffledCards = cardValues.OrderBy(x => rnd.Next()).Select(value => new Card { Value = value }).ToList();

            _currentGame = new Game
            {
                Cards = shuffledCards,
                Player1Score = 0,
                Player2Score = 0,
                CurrentPlayer = rnd.Next(1, 3),
                IsGameOver = false
            };

            return _currentGame;
        }

        public Game GetGameState() => _currentGame;

        public bool PlayTurn(int cardPosition1, int cardPosition2)
        {
            if (_currentGame == null || _currentGame.IsGameOver || cardPosition1 == cardPosition2 ||
                _currentGame.Cards[cardPosition1].IsMatched || _currentGame.Cards[cardPosition2].IsMatched)
            {
                return false;
            }

            var card1 = _currentGame.Cards[cardPosition1];
            var card2 = _currentGame.Cards[cardPosition2];

            card1.IsFlipped = card2.IsFlipped = true;

            if (card1.Value == card2.Value)
            {
                card1.IsMatched = card2.IsMatched = true;
                if (_currentGame.CurrentPlayer == 1) _currentGame.Player1Score++;
                else _currentGame.Player2Score++;
                CheckGameOver();
                return true;
            }
            else
            {
                _currentGame.CurrentPlayer = _currentGame.CurrentPlayer == 1 ? 2 : 1;
                return false;
            }
        }

        private void CheckGameOver()
        {
            _currentGame.IsGameOver = _currentGame.Cards.All(c => c.IsMatched);
        }
    }
}

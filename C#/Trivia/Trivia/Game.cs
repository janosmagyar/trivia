﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private const int CoinsToWin = 6;

        private readonly Action<string> _output;
        private readonly List<Player> _players = new List<Player>();

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;
        private const int BoardSize = 12;
        private const int QuestionCount = 50;
        private Board _board;

        public Game(Action<string> output)
        {
            _output = output;
            _board = new Board(
                BoardSize,
                new[]
                    {
                        new Category(Categories.Pop, QuestionCount),
                        new Category(Categories.Science, QuestionCount),
                        new Category(Categories.Sports, QuestionCount),
                        new Category(Categories.Rock,QuestionCount),
                    });
        }

        public bool IsPlayable()
        {
            return _players.Count >= 2;
        }

        public void AddPlayer(string playerName)
        {
            _players.Add(new Player(playerName));
            _output(playerName + " was added");
            _output("They are player number " + _players.Count);
        }

        public void Roll(int roll)
        {
            _output(_players[_currentPlayer].Name + " is the current player");
            _output("They have rolled a " + roll);

            if (HandlePenaltyBox(roll)) return;
            AskQuestion(roll);
        }

        private bool HandlePenaltyBox(int roll)
        {
            if (!_players[_currentPlayer].IsInPenaltyBox) return false;
            
            if (roll % 2 != 0)
            {
                _isGettingOutOfPenaltyBox = true;

                _output(_players[_currentPlayer].Name + " is getting out of the penalty box");
            }
            else
            {
                _output(_players[_currentPlayer].Name + " is not getting out of the penalty box");
                _isGettingOutOfPenaltyBox = false;
                return true;
            }
            return false;
        }

        private void AskQuestion(int roll)
        {
            _players[_currentPlayer].Place += roll;
            if (_players[_currentPlayer].Place > BoardSize - 1) _players[_currentPlayer].Place = _players[_currentPlayer].Place - BoardSize;

            _output(_players[_currentPlayer].Name
                    + "'s new location is "
                    + _players[_currentPlayer].Place);
            _output("The category is " + _board.Category(_players[_currentPlayer].Place));
            var cc = _board.Category(_players[_currentPlayer].Place);
            var q =   _board.Categories[cc].Questions.First();
            _output(q);
            _board.Categories[cc].Questions.Remove(q);
        }

       

        public bool WasCorrectlyAnswered()
        {
            return _players[_currentPlayer].IsInPenaltyBox 
                ? PenaltyBoxAnswer() 
                : NormalAnswer();
        }

        private bool NormalAnswer()
        {
            _output("Answer was correct!!!!");
            _players[_currentPlayer].Purse++;
            _output(_players[_currentPlayer].Name
                    + " now has "
                    + _players[_currentPlayer].Purse
                    + " Gold Coins.");

            var winner = DidPlayerWin();
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;

            return winner;
        }

        private bool PenaltyBoxAnswer()
        {
            if (_isGettingOutOfPenaltyBox)
            {
                return NormalAnswer();
            }
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }

        public bool WrongAnswer()
        {
            _output("Question was incorrectly answered");
            _output(_players[_currentPlayer].Name + " was sent to the penalty box");
            _players[_currentPlayer].IsInPenaltyBox = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return _players[_currentPlayer].Purse != CoinsToWin;
        }
    }

}

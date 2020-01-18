﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        
        
        private readonly Action<string> _output;
        private readonly List<Player> _players = new List<Player>();

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;
        private const int BoardSize = 12;
        private const int QuestionCount = 50;
        private Board _board;

        public Game(Action<string> output, IMarket market)
        {
            _output = output;
            _board = new Board(BoardSize, market.Categories);
        }

        public bool IsPlayable()
        {
            return _players.Count >= 2;
        }

        public void AddPlayer(string playerName)
        {
            _players.Add(new Player(playerName));
            _board.AddPlayer(playerName);
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
            _board.MovePlayer(_players[_currentPlayer].Name, roll);
            _output(_players[_currentPlayer].Name
                    + "'s new location is "
                    + _board.PlayerLocations[_players[_currentPlayer].Name]);
            _output("The category is " + _board.Category(_board.PlayerLocations[_players[_currentPlayer].Name]));
            var cc = _board.Category(_board.PlayerLocations[_players[_currentPlayer].Name]);
            
            var q = _board.Categories[cc].Questions[_board.Categories[cc].QuestionsUsed];
            _board.Categories[cc].QuestionsUsed++;
            _output(q);
            
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
            var category = _board.Category(_players[_currentPlayer].Name);
            _players[_currentPlayer].Purse += category.Score;
            _output(_players[_currentPlayer].Name
                    + " now has "
                    + _players[_currentPlayer].Purse
                    + " Gold Coins.");

            var winner = DidPlayerNotWin();
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
            Penalty(_board,_output,_players[_currentPlayer]);
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }

        public void Penalty(Board board, Action<string> output, Player currentPlayer)
        {
            var cc = board.Category(board.PlayerLocations[currentPlayer.Name]);
            if (currentPlayer.PlayerType == PlayerType.Adult || cc == CategoryNames.Pop)
            {
                output( $"{currentPlayer.Name} was sent to the penalty box");
                currentPlayer.IsInPenaltyBox = true;
            }
        }


        private bool DidPlayerNotWin()
        {
            var currentPlayer = _players[_currentPlayer];
            return !new WinningConditions().IsWining(currentPlayer);
        }
    }
}

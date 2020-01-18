﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private const string PopCategory = "Pop";
        private const string SienceCategory = "Science";
        private const string SportsCategory = "Sports";
        private const string RockCategory = "Rock";
        private readonly Action<string> _output;
        private readonly List<string> _players = new List<string>();

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];

        private readonly bool[] _inPenaltyBox = new bool[6];

        private readonly Dictionary<string,IList<string>> _questions = new Dictionary<string,IList<string>>()
        {
            {PopCategory,new List<string>()},
            {SienceCategory, new List<string>()},
            {SportsCategory, new List<string>()},
            {RockCategory,new List<string>()},
        };

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public Game(Action<string> output)
        {
            _output = output;
            for (var i = 0; i < 50; i++)
            {
                _questions[PopCategory].Add("Pop Question " + i);
                _questions[SienceCategory].Add(("Science Question " + i));
                _questions[SportsCategory].Add(("Sports Question " + i));
                _questions[RockCategory].Add(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public bool Add(string playerName)
        {
            _players.Add(playerName);
            _places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

            _output(playerName + " was added");
            _output("They are player number " + _players.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            _output(_players[_currentPlayer] + " is the current player");
            _output("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    _output(_players[_currentPlayer] + " is getting out of the penalty box");
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                    _output(_players[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    _output("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    _output(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _places[_currentPlayer] = _places[_currentPlayer] + roll;
                if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                _output(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                _output("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            switch (CurrentCategory())
            {
                case PopCategory:
                {
                    var q = _questions[PopCategory].First();
                    _output(q);
                    _questions[PopCategory].Remove(q);
                    break;
                }
                case SienceCategory:
                {
                    var q = _questions[SienceCategory].First();
                    _output(q);
                    _questions[SienceCategory].Remove(q);
                    break;
                }
                case SportsCategory:
                {
                    var q = _questions[SportsCategory].First();
                    _output(q);
                    _questions[SportsCategory].Remove(q);
                    break;
                }
                case RockCategory:
                {
                    var q = _questions[RockCategory].First();
                    _output(q);
                    _questions[RockCategory].Remove(q);
                    break;
                }
            }
        }

        private string CurrentCategory()
        {
            if (_places[_currentPlayer] == 0) return PopCategory;
            if (_places[_currentPlayer] == 4) return PopCategory;
            if (_places[_currentPlayer] == 8) return PopCategory;
            if (_places[_currentPlayer] == 1) return SienceCategory;
            if (_places[_currentPlayer] == 5) return SienceCategory;
            if (_places[_currentPlayer] == 9) return SienceCategory;
            if (_places[_currentPlayer] == 2) return SportsCategory;
            if (_places[_currentPlayer] == 6) return SportsCategory;
            if (_places[_currentPlayer] == 10) return SportsCategory;
            return RockCategory;
        }

        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    _output("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    _output(_players[_currentPlayer]
                            + " now has "
                            + _purses[_currentPlayer]
                            + " Gold Coins.");

                    var winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                _output("Answer was corrent!!!!");
                _purses[_currentPlayer]++;
                _output(_players[_currentPlayer]
                        + " now has "
                        + _purses[_currentPlayer]
                        + " Gold Coins.");

                var winner = DidPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            _output("Question was incorrectly answered");
            _output(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private const int MaxPlayers = 6;
        private const int CoinsToWin = 6;

        private readonly Action<string> _output;
        private readonly List<string> _players = new List<string>();

        private readonly int[] _places = new int[MaxPlayers];
        private readonly int[] _purses = new int[MaxPlayers];

        private readonly bool[] _inPenaltyBox = new bool[MaxPlayers];

        private readonly Dictionary<string,IList<string>> _questions = new Dictionary<string,IList<string>>()
        {
            {Categories.Pop,new List<string>()},
            {Categories.Science, new List<string>()},
            {Categories.Sports, new List<string>()},
            {Categories.Rock,new List<string>()},
        };

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;
        private const int CategoryCount  = 4;
        private const int BoardSize = 12;

        public Game(Action<string> output)
        {
            _output = output;
            for (var i = 0; i < 50; i++)
            {
                
                AddQuestion(Categories.Pop,i);
                AddQuestion(Categories.Science,i);
                AddQuestion(Categories.Sports,i);
                AddQuestion(Categories.Rock,i);
            }
        }

        private void AddQuestion(string category, int index)
        {
            _questions[category].Add($"{category} Question {index}");
        }

        public bool IsPlayable()
        {
            return _players.Count >= 2;
        }

        public void AddPlayer(string playerName)
        {
            _players.Add(playerName);
            _output(playerName + " was added");
            _output("They are player number " + _players.Count);
        }

        public void Roll(int roll)
        {
            _output(_players[_currentPlayer] + " is the current player");
            _output("They have rolled a " + roll);

            if (HandlePenaltyBox(roll)) return;
            AskQuestion(roll);
        }

        private bool HandlePenaltyBox(int roll)
        {
            if (!_inPenaltyBox[_currentPlayer]) return false;
            
            if (roll % 2 != 0)
            {
                _isGettingOutOfPenaltyBox = true;

                _output(_players[_currentPlayer] + " is getting out of the penalty box");
            }
            else
            {
                _output(_players[_currentPlayer] + " is not getting out of the penalty box");
                _isGettingOutOfPenaltyBox = false;
                return true;
            }
            return false;
        }

        private void AskQuestion(int roll)
        {
            _places[_currentPlayer] = _places[_currentPlayer] + roll;
            if (_places[_currentPlayer] > BoardSize - 1) _places[_currentPlayer] = _places[_currentPlayer] - BoardSize;

            _output(_players[_currentPlayer]
                    + "'s new location is "
                    + _places[_currentPlayer]);
            _output("The category is " + CurrentCategory());
            var cc = CurrentCategory();
            var q = _questions[cc].First();
            _output(q);
            _questions[cc].Remove(q);
        }

        private string CurrentCategory()
        {
            var categoryIndex = _places[_currentPlayer] % CategoryCount;
            var categoryMap = new Dictionary<int, string>()
            {
                {0, Categories.Pop},
                {1, Categories.Science},
                {2, Categories.Sports},
                {3, Categories.Rock},
            };
            return categoryMap[categoryIndex];
        }

        public bool WasCorrectlyAnswered()
        {
            return _inPenaltyBox[_currentPlayer] 
                ? PenaltyBoxAnswer() 
                : NormalAnswer();
        }

        private bool NormalAnswer()
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
            _output(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return _purses[_currentPlayer] != CoinsToWin;
        }
    }

}

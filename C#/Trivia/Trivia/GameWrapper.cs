using System;

namespace Trivia
{
    public class GameWrapper
    {
        private  bool _notAWinner;
        private readonly Random _rand;

        public GameWrapper(Action<string> output, int? seed =null)
        {
            _rand = seed.HasValue
                ? new Random(seed.Value)
                : new Random();
        }

        public void Run()
        {
            var aGame = new Game();

            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            do
            {
                aGame.Roll(_rand.Next(5) + 1);

                if (_rand.Next(9) == 7)
                {
                    _notAWinner = aGame.WrongAnswer();
                }
                else
                {
                    _notAWinner = aGame.WasCorrectlyAnswered();
                }
            } while (_notAWinner);
        }
    }
}
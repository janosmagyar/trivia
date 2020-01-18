using System;

namespace Trivia
{
    public class GameWrapper
    {
        private readonly Action<string> _output;
        private readonly IMarket _market;
        private  bool _notAWinner;
        private readonly Random _rand;
        
        public GameWrapper(Action<string> output,IMarket market, int? seed =null)
        {
            _output = output;
            _market = market;
            _rand = seed.HasValue
                ? new Random(seed.Value)
                : new Random();
        }

        public void Run()
        {
            var aGame = new Game(_output,_market);

            aGame.AddPlayer("Chet");
            aGame.AddPlayer("Pat");
            aGame.AddPlayer("Sue");

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
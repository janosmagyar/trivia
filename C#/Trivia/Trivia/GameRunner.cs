using System;

namespace Trivia
{
    public class GameRunner
    {
        public static void Main(string[] args)
        {
            new GameWrapper(Console.WriteLine).Run();
        }
    }
}
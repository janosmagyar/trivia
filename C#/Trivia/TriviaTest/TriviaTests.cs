using System;
using System.IO;
using Trivia;
using Xunit;

namespace TriviaTest
{
    
    public class TriviaTests
    {
        [Fact(Skip = "Only at first time")]
        public void GenerateFacts()
        {
            using (var output = new StreamWriter("testoutput.txt"))
            {
                Console.SetOut(output);
                for (int seed = 0; seed < 1000; seed++)
                {
                    GameRunner.Main(new string[] {seed.ToString()});
                }
            }
        }
    }
}
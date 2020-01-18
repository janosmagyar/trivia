using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using Trivia;
using Xunit;

namespace TriviaTest
{
    
    [UseReporter(typeof(DiffReporter))]
    public class TriviaTests
    {
        [Fact]
        public void CheckFacts()
        {
            using (var output = new StringWriter())
            {
                Console.SetOut(output);
                for (int seed = 0; seed < 1000; seed++)
                {
                    GameRunner.Main(new string[] {seed.ToString()});
                }
                Approvals.Verify(output);
            }
        }
    }
}
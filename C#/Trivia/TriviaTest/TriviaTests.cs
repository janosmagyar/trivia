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
                    new GameWrapper(Console.WriteLine,seed).Run();
                }
                Approvals.Verify(output);
            }
        }
    }
}
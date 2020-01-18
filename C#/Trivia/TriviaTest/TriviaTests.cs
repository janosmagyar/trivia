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
                for (int seed = 0; seed < 1000; seed++)
                {
                    new GameWrapper(output.WriteLine,new InternationalMarket(),seed).Run();
                }
                Approvals.Verify(output);
            }
        }
    }
}
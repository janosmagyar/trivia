using System.Collections.Generic;

namespace Trivia
{
    public class Category
    {
        public int QuestionsUsed { get; set; }
        public string  Name { get;}
        public int Score { get; }
        public IList<string> Questions { get; } = new List<string>();
        
        public Category(string name, int score, int quiestionCount)
        {
            Name = name;
            Score = score;
            for (var i = 0; i < quiestionCount; i++)
            {
                Questions.Add($"{Name} Question {i}");
            }
        }
    }
}
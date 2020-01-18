using System.Collections.Generic;

namespace Trivia
{
    public class Category
    {

        public string  Name { get;}
        public IList<string> Questions { get; } = new List<string>();
        
        public Category(string name, int quiestionCount)
        {
            Name = name;
            for (var i = 0; i < quiestionCount; i++)
            {
                Questions.Add($"{Name} Question {i}");
            }
        }
    }
}
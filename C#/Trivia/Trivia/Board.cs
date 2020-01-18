using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Board
    {
        private readonly int _size;
        public  Dictionary<string, Category> Categories { get; }
        
        public string Category(int position)
        {
            var categoryIndex = position % Categories.Values.Count;
            var categoryMap = new Dictionary<int, string>()
            {
                {0, Trivia.Categories.Pop},
                {1, Trivia.Categories.Science},
                {2, Trivia.Categories.Sports},
                {3, Trivia.Categories.Rock},
            };
            return categoryMap[categoryIndex];
        }
        public Board(int size, ICollection<Category> categories)
        {
            _size = size;
            Categories = categories.ToDictionary(c => c.Name, c => c);
        }
    }
}
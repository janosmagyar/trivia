using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Board
    {
        private readonly int _size;
        public  Dictionary<string, Category> Categories { get; }
        public Dictionary<string,int> PlayerLocations { get; } = new Dictionary<string, int>();
        private Dictionary<int,string> CategoryMap { get; }  = new Dictionary<int, string>();
        
        public Board(int size, Category[] categories)
        {
            _size = size;
            Categories = categories.ToDictionary(c => c.Name, c => c);
            for (int i = 0; i < categories.Length; i++)
            {
                CategoryMap.Add(i,categories[i].Name);
            }
        }
        
        public string Category(int position)
        {
            var categoryIndex = position % Categories.Values.Count;
            
            return CategoryMap[categoryIndex];
        }

        public void AddPlayer(string name)
        {
            PlayerLocations.Add(name,0);
        }

        public int PlayerLocation(string name)
        {
            return PlayerLocations[name];
        }


        public void MovePlayer(string name, int roll)
        {
            PlayerLocations[name] = (PlayerLocations[name] + roll) % _size;
        }

        
    }
}
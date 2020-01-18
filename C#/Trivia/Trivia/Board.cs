using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Board
    {
        private readonly StepManager _stepManager;
        public  Dictionary<string, Category> Categories { get; }
        public Dictionary<string,int> PlayerLocations { get; } = new Dictionary<string, int>();
        private Dictionary<int,string> CategoryMap { get; }  = new Dictionary<int, string>();
        
        public Board(Category[] categories, StepManager stepManager)
        {
            _stepManager = stepManager;
            Categories = categories.ToDictionary(c => c.Name, c => c);
            for (int i = 0; i < categories.Length; i++)
            {
                CategoryMap.Add(i,categories[i].Name);
            }
        }

        public Category Category(string playerName)
        {
            return Categories[Category(PlayerLocations[playerName])];
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
            PlayerLocations[name] = _stepManager.Step(PlayerLocations[name], roll);
        }
    }
}
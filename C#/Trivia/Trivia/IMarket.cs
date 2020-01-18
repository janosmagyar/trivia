namespace Trivia
{
    public interface IMarket
    {
        Category[] Categories { get; }
    }
    
    public class InternationalMarket:IMarket
    {
        public Category[] Categories { get; }

        public InternationalMarket()
        {
            var questionCount = 50;

            Categories = new[]
            {
                new Category(CategoryNames.Pop, 1, questionCount),
                new Category(CategoryNames.Science, 1, questionCount),
                new Category(CategoryNames.Sports, 1, questionCount),
                new Category(CategoryNames.Rock, 1, questionCount),
            };
        }
    }
    
    public class UsMarket:IMarket
    {
        public Category[] Categories { get; }

        public UsMarket()
        {
            var questionCount = 50;

            Categories = new[]
            {
                new Category(CategoryNames.Pop, 1, questionCount),
                new Category(CategoryNames.Politics, 1, questionCount),
                new Category(CategoryNames.Sports, 1, questionCount),
                new Category(CategoryNames.Rock, 1, questionCount),
            };
        }
    }
    
    public class GermanMarket:IMarket
    {
        public Category[] Categories { get; }

        public GermanMarket()
        {
            var questionCount = 50;

            Categories = new[]
            {
                new Category(CategoryNames.Pop, 1, questionCount),
                new Category(CategoryNames.Science, 2, questionCount),
                new Category(CategoryNames.Sports, 1, questionCount),
                new Category(CategoryNames.Rock, 1, questionCount),
            };
        }
    }
}
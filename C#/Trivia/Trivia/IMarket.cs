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
                new Category(CategoryNames.Pop, questionCount),
                new Category(CategoryNames.Science, questionCount),
                new Category(CategoryNames.Sports, questionCount),
                new Category(CategoryNames.Rock, questionCount),
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
                new Category(CategoryNames.Pop, questionCount),
                new Category(CategoryNames.Politics, questionCount),
                new Category(CategoryNames.Sports, questionCount),
                new Category(CategoryNames.Rock, questionCount),
            };
        }
    }
}
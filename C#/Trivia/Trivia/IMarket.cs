namespace Trivia
{
    public interface IMarket
    {
        Category[] Categories { get; }
        StepManager StepManager { get; }
    }
    
    public class InternationalMarket:IMarket
    {
        public Category[] Categories { get; }
        public StepManager StepManager { get; }

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
            StepManager = new NormalStepManager(12);
        }
    }
    
    public class UsMarket:IMarket
    {
        public Category[] Categories { get; }
        public StepManager StepManager { get; }

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
            StepManager = new NormalStepManager(12);
        }
    }
    
    public class GermanMarket:IMarket
    {
        public Category[] Categories { get; }
        public StepManager StepManager { get; }

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
            StepManager = new NormalStepManager(12);
        }

        public class ChineseMarket : IMarket
        {
            public Category[] Categories { get; }
            public StepManager StepManager { get; }

            public ChineseMarket()
            {
                var questionCount = 50;

                Categories = new[]
                {
                    new Category(CategoryNames.Pop, 1, questionCount),
                    new Category(CategoryNames.Science, 2, questionCount),
                    new Category(CategoryNames.Sports, 1, questionCount),
                    new Category(CategoryNames.Rock, 1, questionCount),
                };
                StepManager = new ChineseStepManager(12);
            }
        }
    }
}

    
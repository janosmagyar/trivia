namespace Trivia
{
    public abstract class StepManager
    {
        protected int Size { get; }

        protected StepManager(int size)
        {
            Size = size;
        }
        public abstract int Step(int startPosition, int roll);
    }

    public class NormalStepManager:StepManager
    {
        public NormalStepManager(int size) : base(size)
        {
        }

        public override int Step(int startPosition, int roll)
        {
            return (startPosition + roll) % Size;
        }
    }
    
    public class ChineseStepManager : StepManager
    {
        public ChineseStepManager(int size) : base(size)
        {
            
        }

        public override int Step(int startPosition, int roll)
        {
            int currentPosition = startPosition;
            for (int i = startPosition; i < startPosition+roll; i++)
            {
                currentPosition++;
                if (currentPosition == 4) currentPosition++;
                if (currentPosition == Size) currentPosition = 0;
            }

            return currentPosition;
        }
    }
}
namespace SignRecognition.Domain.Exceptions
{
    public class TrainingDataNotFoundException : System.Exception
    {
        public TrainingDataNotFoundException()
            : base("Training data not found")
        {
        }

        public TrainingDataNotFoundException(string message)
            : base(message)
        {
        }

        public TrainingDataNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
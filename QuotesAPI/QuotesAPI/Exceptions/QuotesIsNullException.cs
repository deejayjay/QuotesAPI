namespace QuotesAPI.Exceptions
{
    public class QuotesIsNullException : Exception
    {
        public QuotesIsNullException(string message):base(message)
        {
        }

        public QuotesIsNullException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

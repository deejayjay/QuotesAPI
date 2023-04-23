namespace QuotesAPI.Exceptions
{
    public class QuoteNotFoundException : Exception
    {
        public QuoteNotFoundException(string message):base(message)
        {
        }

        public QuoteNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

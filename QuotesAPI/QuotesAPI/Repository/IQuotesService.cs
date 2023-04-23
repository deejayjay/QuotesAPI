using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using QuotesAPI.Models;

namespace QuotesAPI.Repository
{
    public interface IQuotesService
    {
        public Task<ActionResult<IEnumerable<QuoteTable>>> GetAllQuotes();
        public Task<ActionResult<IEnumerable<QuoteTable>>> GetQuotesByAuthor(string author);
        public Task<ActionResult<QuoteTable>> GetQuoteById(int id);
        public Task AddQuote(QuoteTable newQuote);
        public Task UpdateQuote(int id, JsonPatchDocument updatedQuote);
        public Task DeleteQuote(int id);
    }
}

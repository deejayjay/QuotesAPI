using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesAPI.Data;
using QuotesAPI.Exceptions;
using QuotesAPI.Models;

namespace QuotesAPI.Repository
{
    public class QuotesService : IQuotesService
    {
        private readonly QuotesDbContext _context;
        public QuotesService(QuotesDbContext context)
        {
            _context = context;
        }
        public async Task AddQuote(QuoteTable newQuote)
        {
            if (_context.Quotes == null)
            {
                throw new QuotesIsNullException("Entity set 'QuotesDbContext.Quotes'  is null.");
            }
            _context.Quotes.Add(newQuote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuote(int id)
        {
            if (_context.Quotes == null)
            {
                throw new QuotesIsNullException("Entity set 'QuotesDbContext.Quotes'  is null.");
            }

            var quoteTable = await _context.Quotes.FindAsync(id);
            if (quoteTable == null)
            {
                throw new QuoteNotFoundException("A quote with the given id does not exist");
            }

            _context.Quotes.Remove(quoteTable);
            await _context.SaveChangesAsync();            
        }

        public async Task<ActionResult<IEnumerable<QuoteTable>>> GetAllQuotes()
        {
            if (_context.Quotes == null)
            {
                throw new QuotesIsNullException("Entity set 'QuotesDbContext.Quotes'  is null.");
            }

            return await _context.Quotes.ToListAsync();
        }

        public async Task<ActionResult<QuoteTable>> GetQuoteById(int id)
        {
            if (_context.Quotes == null)
            {
                throw new QuotesIsNullException("Entity set 'QuotesDbContext.Quotes'  is null.");
            }
            var quoteTable = await _context.Quotes.FindAsync(id);

            if (quoteTable == null)
            {
                throw new QuoteNotFoundException("A quote with the given id does not exist");
            }

            return quoteTable;
        }

        public Task<ActionResult<IEnumerable<QuoteTable>>> GetQuotesByAuthor(string author)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateQuote(int id, JsonPatchDocument updatedQuote)
        {
            var quoteFromDb = await _context.Quotes.FindAsync(id);
            if(quoteFromDb == null)
            {
                throw new QuoteNotFoundException("A quote with the given id does not exist");
            }
            updatedQuote.ApplyTo(quoteFromDb);
            await _context.SaveChangesAsync();
        }
    }
}

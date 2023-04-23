using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using QuotesAPI.Exceptions;
using QuotesAPI.Models;
using QuotesAPI.Repository;

namespace QuotesAPI.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesService _service;
        
        public QuotesController(IQuotesService service)
        {
            _service = service;            
        }

        // GET: api/Quotes
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuoteTable>>> GetQuotes()
        {
            try
            {
                var allQuotes = await _service.GetAllQuotes();
                return allQuotes;
            }
            catch (QuotesIsNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Quotes/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteTable>> GetQuoteById(int id)
        {
            try
            {
                var quote = await _service.GetQuoteById(id);
                return quote;
            }
            catch (QuotesIsNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch(QuoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PATCH: api/Quotes/5
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateQuote(int id, JsonPatchDocument updatedQuote)
        {
            try
            {
                await _service.UpdateQuote(id, updatedQuote);
                return NoContent();
            }
            catch (QuoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }            
        }

        // POST: api/Quotes
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<QuoteTable>> AddQuote(QuoteTable newQuote)
        {
            if (string.IsNullOrWhiteSpace(newQuote.Quote))
            {
                return BadRequest("Quote cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(newQuote.Author)) 
            {
                return BadRequest("Author cannot be empty");
            }

            try
            {
                await _service.AddQuote(newQuote);
                return Ok(newQuote);
            }
            catch (QuotesIsNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Quotes/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            try
            {
                await _service.DeleteQuote(id);
                return NoContent();
            }
            catch (QuotesIsNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (QuoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

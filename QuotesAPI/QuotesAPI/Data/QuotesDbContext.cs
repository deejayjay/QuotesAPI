using Microsoft.EntityFrameworkCore;
using QuotesAPI.Models;

namespace QuotesAPI.Data
{
    public class QuotesDbContext : DbContext
    {
        public QuotesDbContext(DbContextOptions<QuotesDbContext> options) : base(options)
        {
        }

        public DbSet<QuoteTable> Quotes { get; set; } = null!;
    }
}

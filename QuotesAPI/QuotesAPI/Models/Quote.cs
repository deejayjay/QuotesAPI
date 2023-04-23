using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotesAPI.Models
{
    [Table("Quotes")]
    public class QuoteTable
    {
        [Key]
        public int Id { get; set; }
        public string? Quote { get; set; }
        public string? Author { get; set; }
    }
}

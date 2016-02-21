using System.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace GetAutoRefreshedImage.Models
{
    public class ContextDB : DbContext
    {
        public ContextDB() : base("name=MailImageAutoRefreshed")
        {
            
        }

        public DbSet<UrlsDatesMapping> UrlsDatesMapping { get; set; }
    }
    public partial class UrlsDatesMapping
    {
        [Key]
        public string IdUrl { get; set; }
        public DateTime EventDateTime { get; set; }
    }
}
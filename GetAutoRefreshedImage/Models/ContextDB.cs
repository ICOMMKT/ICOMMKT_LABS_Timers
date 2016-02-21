using System.Data.Entity;

namespace GetAutoRefreshedImage.Models
{
    public class ContextDB : DbContext
    {
        public ContextDB() : base("name=MailImageAutoRefreshed")
        {
            
        }

        //public DbSet<ImageCropData> ImageCropData { get; set; }
    }
}
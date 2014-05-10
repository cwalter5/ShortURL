using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SURL.Models
{
    public class UrlModel
    {
        public int Id { get; set; }

        [Display(Name="URL to shorten.")]
        [DataType(DataType.Url)]
        [Required(ErrorMessage="Url is required.")]
        [StringLength(2048, MinimumLength=1, ErrorMessage="Url must be between 1 and 2048 characters.")]
        public string Url { get; set; }
    }

    public class UrlModelDbContext : DbContext
    {
        public DbSet<UrlModel> Urls {get; set;}
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.RestApi.Models
{
  
    [Table("tbook")]
    public class Book
    {
        [Key]
        [Column("nBookID")]
        public int Id { get; set; }

        [Column("cTitle")]
        public string Title { get; set; }

        [Column("nAuthorID")]
        public int AuthorId { get; set; }

        [Column("nPublishingYear")]
        public int PublishingYear { get; set; }

        [Column("nPublishingCompanyID")]
        public int PublishingCompanyId { get; set; }
    }
}
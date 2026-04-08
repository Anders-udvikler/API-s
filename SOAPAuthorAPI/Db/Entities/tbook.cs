using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library.SoapApi.Db.Entities
{

    [Index(nameof(nAuthorID), Name = "idx_tbook_IDX_BOOK_AUTHOR_ID")]
    [Index(nameof(nPublishingCompanyID), Name = "idx_tbook_IDX_BOOK_PUBLISHING_COMPANY_ID")]
    [Table("tbook")]
    public class Book
    {
        [Key]
        public int nBookID { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string cTitle { get; set; } = "Unknown";

        public int nAuthorID { get; set; }

        public int nPublishingCompanyID { get; set; }

        [Column(TypeName = "decimal(4)")]
        public decimal? nPublishingYear { get; set; }
    }

}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.SoapApi.Db.Entities
{
    [Table("tauthor")]

    public class Author
    {
        [Key]
        public int nAuthorID { get; set; }

        [Required]
        [StringLength(40)]
        [Column(TypeName = "varchar(40)")]
        public string cName { get; set; } = "Unknown";

        [StringLength(60)]
        [Column(TypeName = "varchar(60)")]
        public string? cSurname { get; set; }
    }
}

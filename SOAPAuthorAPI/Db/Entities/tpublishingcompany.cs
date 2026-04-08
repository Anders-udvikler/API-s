using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.SoapApi.Db.Entities
{
    [Table("tpublishingcompany")]
    public class Publishingcompany
    {
        [Key]
        public int nPublishingCompanyID { get; set; }

        [Required]
        [StringLength(40)]
        [Column(TypeName = "varchar(40)")]
        public string cName { get; set; } = "Unknown";
    }
}

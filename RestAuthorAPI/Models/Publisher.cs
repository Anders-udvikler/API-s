using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.RestApi.Models
{
    /// Maps to table: tpublishingcompany
   
    [Table("tpublishingcompany")]
    public class Publisher
    {
        [Key]
        [Column("nPublishingCompanyID")]
        public int Id { get; set; }

        [Column("cName")]
        public string Name { get; set; }
    }
}
using System.Runtime.Serialization;
namespace Library.SoapApi.Models
{
    [DataContract(Name = "Book")]
    public class BookDto
    {
        [DataMember(Name = "AuthorId", Order = 1)]
        public int AuthorId { get; set; }
        
        [DataMember(Name = "Id", Order = 2)]
        public int Id { get; set; }
        
        [DataMember(Name = "PublishingCompanyId", Order = 3)]
        public int PublishingCompanyId { get; set; }
        
        [DataMember(Name = "PublishingYear", Order = 4)]
        public int PublishingYear { get; set; }
        
        [DataMember(Name = "Title", Order = 5, IsRequired = true)]
        public required string Title { get; set; }
    }
}
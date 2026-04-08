using System.Runtime.Serialization;
namespace Library.SoapApi.Models
{
    [DataContract(Name = "Book")]
    public class BookDto
    {
        [DataMember(Name = "id", Order = 1)]
        public int Id { get; set; }
        
        [DataMember(Name = "title", Order = 2, IsRequired = true)]
        public required string Title { get; set; }
        
        [DataMember(Name = "authorId", Order = 3)]
        public int AuthorId { get; set; }
        
        [DataMember(Name = "publishingCompanyId", Order = 4)]
        public int PublishingCompanyId { get; set; }
        
        [DataMember(Name = "publishingYear", Order = 5)]
        public int PublishingYear { get; set; }
    }
}

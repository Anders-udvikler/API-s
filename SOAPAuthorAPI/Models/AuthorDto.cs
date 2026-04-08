using System.Runtime.Serialization;

namespace Library.SoapApi.Models
{
    [DataContract(Name = "Author")]
    public class AuthorDto
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "Name", Order = 2, IsRequired = true)]
        public required string Name { get; set; }

        [DataMember(Name = "Surname", Order = 3, IsRequired = true)]
        public required string Surname { get; set; }
    }
}
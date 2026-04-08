using System.Runtime.Serialization;

namespace Library.SoapApi.Models
{
    [DataContract(Name = "PublishingCompany")]
    public class PublishingCompanyDto
    {
        [DataMember(Name = "id", Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "name", Order = 2, IsRequired = true)]
        public required string Name { get; set; }
    }
}
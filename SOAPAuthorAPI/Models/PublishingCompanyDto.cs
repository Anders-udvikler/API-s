using System.Runtime.Serialization;

namespace Library.SoapApi.Models
{
    [DataContract(Name = "PublishingCompany")]
    public class PublishingCompanyDto
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "Name", Order = 2, IsRequired = true)]
        public required string Name { get; set; }
    }
}
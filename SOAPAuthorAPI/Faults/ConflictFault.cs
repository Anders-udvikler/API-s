using System.Runtime.Serialization;

namespace Library.SoapApi.Faults
{
    [DataContract]
    public class ConflictFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}
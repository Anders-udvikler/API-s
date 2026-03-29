using System.Runtime.Serialization;

namespace Library.SoapApi.Faults
{
    [DataContract]
    public class NotFoundFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}
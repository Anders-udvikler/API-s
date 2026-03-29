using System.Runtime.Serialization;

namespace Library.SoapApi.Faults
{
    [DataContract]
    public class ValidationFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}
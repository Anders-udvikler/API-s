using System.ServiceModel;

[ServiceContract]
public interface ILibrarySoapService
{
    [OperationContract]
    string Ping();
}

public class LibrarySoapService : ILibrarySoapService
{
    public string Ping()
    {
        return "SOAP is working!";
    }
}
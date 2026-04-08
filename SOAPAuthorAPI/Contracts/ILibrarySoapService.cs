using Library.SoapApi.Faults;
using Library.SoapApi.Models;
using System.ServiceModel;

namespace Library.SoapApi.Contracts
{
    [ServiceContract(Namespace = "http://library.soapapi.org/")]
    public interface ILibrarySoapService
    {
        // ==================== BOOK ====================

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        int CreateBook(string title, int authorId, int publishingCompanyId, int publishingYear);

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        BookDto GetBookById(int id);

        [OperationContract]
        List<BookDto> ListBooks();

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(NotFoundFault))]
        bool UpdateBook(BookDto book); // bool = success acknowledgement

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        bool DeleteBook(int id);

        // ==================== AUTHOR ====================

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        int CreateAuthor(string name, string surname);

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        AuthorDto GetAuthorById(int id);

        [OperationContract]
        List<AuthorDto> ListAuthors();

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(NotFoundFault))]
        bool UpdateAuthor(AuthorDto author);

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(ConflictFault))]
        bool DeleteAuthor(int id);

        // ==================== PUBLISHING COMPANY ====================

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        int CreatePublishingCompany(string name);

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        PublishingCompanyDto GetPublishingCompanyById(int id);

        [OperationContract]
        List<PublishingCompanyDto> ListPublishingCompanies();

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(NotFoundFault))]
        bool UpdatePublishingCompany(PublishingCompanyDto company);

        [OperationContract]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(ConflictFault))]
        bool DeletePublishingCompany(int id);
    }
}
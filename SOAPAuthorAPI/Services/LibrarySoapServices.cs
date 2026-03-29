using Library.SoapApi.Contracts;
using Library.SoapApi.Db;
using Library.SoapApi.Db.Entities;
using Library.SoapApi.Faults;
using Library.SoapApi.Mapping;
using Library.SoapApi.Models;
using System.ServiceModel;

namespace Library.SoapApi.Services
{
    public class LibrarySoapService : ILibrarySoapService
    {
        private readonly AppDbContext _db;

        private static FaultException<ValidationFault> ValidationFault(string message) =>
            new FaultException<ValidationFault>(
                new ValidationFault { Message = message },
                new FaultReason(message));

        private static FaultException<NotFoundFault> NotFoundFault(string message) =>
            new FaultException<NotFoundFault>(
                new NotFoundFault { Message = message },
                new FaultReason(message));

        private static FaultException<ConflictFault> ConflictFault(string message) =>
            new FaultException<ConflictFault>(
                new ConflictFault { Message = message },
                new FaultReason(message));

        public LibrarySoapService(AppDbContext db)
        {
            _db = db;
        }

        // ==================== BOOK ====================

        public int CreateBook(string title, int authorId, int publishingCompanyId, int publishingYear)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw ValidationFault("Title is required");
            }

            if (publishingYear < 1900)
            {
                throw ValidationFault("Publishing year must be >= 1900");
            }

            var book = new tbook
            {
                cTitle = title,
                nAuthorID = authorId,
                nPublishingCompanyID = publishingCompanyId,
                nPublishingYear = publishingYear
            };

            _db.tbook.Add(book);
            _db.SaveChanges();

            return book.nBookID;
        }

        public BookDto GetBookById(int id)
        {
            var book = _db.tbook.FirstOrDefault(b => b.nBookID == id);

            if (book == null)
            {
                throw NotFoundFault("Book not found");
            }

            return Mapper.ToDto(book);
        }

        public List<BookDto> ListBooks()
        {
            return _db.tbook
                .Select(b => Mapper.ToDto(b))
                .ToList();
        }

        public bool UpdateBook(BookDto book)
        {
            if (book == null || string.IsNullOrWhiteSpace(book.Title))
            {
                throw ValidationFault("Invalid book data");
            }

            if (book.PublishingYear < 1900)
            {
                throw ValidationFault("Publishing year must be >= 1900");
            }

            var existing = _db.tbook.FirstOrDefault(b => b.nBookID == book.Id);

            if (existing == null)
            {
                throw NotFoundFault("Book not found");
            }

            existing.cTitle = book.Title;
            existing.nAuthorID = book.AuthorId;
            existing.nPublishingCompanyID = book.PublishingCompanyId;
            existing.nPublishingYear = book.PublishingYear.HasValue ? book.PublishingYear.Value : null;

            _db.SaveChanges();

            return true;
        }

        public bool DeleteBook(int id)
        {
            var book = _db.tbook.FirstOrDefault(b => b.nBookID == id);

            if (book == null)
            {
                throw NotFoundFault("Book not found");
            }

            _db.tbook.Remove(book);
            _db.SaveChanges();

            return true;
        }

        // ==================== AUTHOR ====================

        public int CreateAuthor(string name, string surname)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
            {
                throw ValidationFault("Name and surname are required");
            }

            var author = new tauthor
            {
                cName = name,
                cSurname = surname
            };

            _db.tauthor.Add(author);
            _db.SaveChanges();

            return author.nAuthorID;
        }

        public AuthorDto GetAuthorById(int id)
        {
            var author = _db.tauthor.FirstOrDefault(a => a.nAuthorID == id);

            if (author == null)
            {
                throw NotFoundFault("Author not found");
            }

            return Mapper.ToDto(author);
        }

        public List<AuthorDto> ListAuthors()
        {
            return _db.tauthor
                .Select(a => Mapper.ToDto(a))
                .ToList();
        }

        public bool UpdateAuthor(AuthorDto author)
        {
            if (author == null || string.IsNullOrWhiteSpace(author.Name) || string.IsNullOrWhiteSpace(author.Surname))
            {
                throw ValidationFault("Invalid author data");
            }

            var existing = _db.tauthor.FirstOrDefault(a => a.nAuthorID == author.Id);

            if (existing == null)
            {
                throw NotFoundFault("Author not found");
            }

            existing.cName = author.Name;
            existing.cSurname = author.Surname;

            _db.SaveChanges();

            return true;
        }

        public bool DeleteAuthor(int id)
        {
            var author = _db.tauthor.FirstOrDefault(a => a.nAuthorID == id);

            if (author == null)
            {
                throw NotFoundFault("Author not found");
            }

            var isUsed = _db.tbook.Any(b => b.nAuthorID == id);

            if (isUsed)
            {
                throw ConflictFault("Cannot delete author: referenced by book");
            }

            _db.tauthor.Remove(author);
            _db.SaveChanges();

            return true;
        }

        // ==================== PUBLISHING COMPANY ====================

        public int CreatePublishingCompany(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw ValidationFault("Name is required");
            }

            var company = new tpublishingcompany
            {
                cName = name
            };

            _db.tpublishingcompany.Add(company);
            _db.SaveChanges();

            return company.nPublishingCompanyID;
        }

        public PublishingCompanyDto GetPublishingCompanyById(int id)
        {
            var company = _db.tpublishingcompany.FirstOrDefault(c => c.nPublishingCompanyID == id);

            if (company == null)
            {
                throw NotFoundFault("Publishing company not found");
            }

            return Mapper.ToDto(company);
        }

        public List<PublishingCompanyDto> ListPublishingCompanies()
        {
            return _db.tpublishingcompany
                .Select(c => Mapper.ToDto(c))
                .ToList();
        }

        public bool UpdatePublishingCompany(PublishingCompanyDto company)
        {
            if (company == null || string.IsNullOrWhiteSpace(company.Name))
            {
                throw ValidationFault("Invalid company data");
            }

            var existing = _db.tpublishingcompany.FirstOrDefault(c => c.nPublishingCompanyID == company.Id);

            if (existing == null)
            {
                throw NotFoundFault("Publishing company not found");
            }

            existing.cName = company.Name;

            _db.SaveChanges();

            return true;
        }

        public bool DeletePublishingCompany(int id)
        {
            var company = _db.tpublishingcompany.FirstOrDefault(c => c.nPublishingCompanyID == id);

            if (company == null)
            {
                throw NotFoundFault("Publishing company not found");
            }

            var isUsed = _db.tbook.Any(b => b.nPublishingCompanyID == id);

            if (isUsed)
            {
                throw ConflictFault("Cannot delete company: referenced by book");
            }

            _db.tpublishingcompany.Remove(company);
            _db.SaveChanges();

            return true;
        }
    }
}
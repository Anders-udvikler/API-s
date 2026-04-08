using Library.SoapApi.Db.Entities;
using Library.SoapApi.Models;

namespace Library.SoapApi.Mapping
{
    public static class Mapper
    {
        public static BookDto ToDto(Book b) => new BookDto
        {
            Id = b.nBookID,
            Title = string.IsNullOrWhiteSpace(b.cTitle) ? "Unknown Title" : b.cTitle,
            AuthorId = b.nAuthorID,
            PublishingCompanyId = b.nPublishingCompanyID,
            PublishingYear = b.nPublishingYear.HasValue ? decimal.ToInt32(b.nPublishingYear.Value) : 1900
        };

        public static Book ToEntity(BookDto b) => new Book
        {
            nBookID = b.Id,
            cTitle = b.Title,
            nAuthorID = b.AuthorId,
            nPublishingCompanyID = b.PublishingCompanyId,
            nPublishingYear = b.PublishingYear
        };

        public static AuthorDto ToDto(Author a) => new AuthorDto
        {
            Id = a.nAuthorID,
            Name = string.IsNullOrWhiteSpace(a.cName) ? "Unknown" : a.cName,
            Surname = string.IsNullOrWhiteSpace(a.cSurname) ? "Unknown" : a.cSurname
        };

        public static PublishingCompanyDto ToDto(Publishingcompany c) => new PublishingCompanyDto
        {
            Id = c.nPublishingCompanyID,
            Name = string.IsNullOrWhiteSpace(c.cName) ? "Unknown" : c.cName
        };
    }
}

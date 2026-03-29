using Library.SoapApi.Db.Entities;
using Library.SoapApi.Models;

namespace Library.SoapApi.Mapping
{
    public static class Mapper
    {
        public static BookDto ToDto(tbook b) => new BookDto
        {
            Id = b.nBookID,
            Title = b.cTitle,
            AuthorId = b.nAuthorID,
            PublishingCompanyId = b.nPublishingCompanyID,
            PublishingYear = b.nPublishingYear.HasValue ? decimal.ToInt32(b.nPublishingYear.Value) : null
        };

        public static tbook ToEntity(BookDto b) => new tbook
        {
            nBookID = b.Id,
            cTitle = b.Title,
            nAuthorID = b.AuthorId,
            nPublishingCompanyID = b.PublishingCompanyId,
            nPublishingYear = b.PublishingYear.HasValue ? b.PublishingYear.Value : null
        };

        public static AuthorDto ToDto(tauthor a) => new AuthorDto
        {
            Id = a.nAuthorID,
            Name = a.cName,
            Surname = a.cSurname
        };

        public static PublishingCompanyDto ToDto(tpublishingcompany c) => new PublishingCompanyDto
        {
            Id = c.nPublishingCompanyID,
            Name = c.cName
        };
    }
}

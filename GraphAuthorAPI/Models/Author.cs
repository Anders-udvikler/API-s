using publishingcompanies;

namespace Auhtors
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PublishingCompanyId { get; set; }
        public int Publishingyear { get; set; }
    }
}
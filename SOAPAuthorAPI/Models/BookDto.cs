namespace Library.SoapApi.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PublishingCompanyId { get; set; }
        public int? PublishingYear { get; set; }
    }
}

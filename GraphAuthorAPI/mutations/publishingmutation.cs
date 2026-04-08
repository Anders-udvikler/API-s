using Auhtors;
using AuthorsRepo;
using Books;
using BooksRepo;
using publishingcompanies;
using publishRepo;
namespace publishingmutation
{
    public class publishingmutation
    {
        public async Task<publishingcompany> AddPublishingCompany( publishingcompany book,
        [Service] publishRepo.publishRepo repo)
    {
        return await repo.AddPublishingCompany(book);
    }
      public async Task<publishingcompany> UpdatePublishingCompany(
        int id, publishingcompany book,
        [Service] publishRepo.publishRepo repo)
        {
            return await repo.UpdatePublishingCompany(book, id);
        }
    
        public async Task<publishingcompany> DeletePublishingCompany(int id,
        [Service] publishRepo.publishRepo repo)
        {
            return await repo.DeletePublishingCompany(id);
        }

                public async Task<Author> AddAuthor(Author au,
        [Service] AuthorRepo repo)
    {
        return await repo.AddAuthor(au);
    }
      public async Task<Author> UpdateAuthor(
        int id, Author author,
        [Service] AuthorRepo repo)
        {
            return await repo.UpdateAuthor(author, id);
        }
    
        public async Task<Author> DeleteAuthor(int id,
        [Service] AuthorRepo repo)
        {
            return await repo.DeleteAuthor(id);
        }

                public async Task<Book> AddBook(
        int id, Book book,
        [Service] BookRepo repo)
    {
        return await repo.AddBook(book);
    }
      public async Task<Book> UpdateBook(
        int id, Book book,
        [Service] BookRepo repo)
        {
            return await repo.UpdateBook(book, id);
        }
    
        public async Task<Book> DeleteBook(int id,
        [Service] BookRepo repo)
        {
            return await repo.DeleteBook(id);
        }

}
}
using Auhtors;
using AuthorsRepo;
using Books;
using BooksRepo;
using publishingcompanies;
using publishRepo;

namespace authorquery
{
    public class publishingquery
    {
        public async Task<List<publishingcompany>> GetAllPublishingCompanies(
        [Service] publishRepo.publishRepo repo)
    {
        var allPublishingCompanies = await repo.GetPublishingCompanies();
        return allPublishingCompanies;
    }

    public async Task<publishingcompany?> GetPublishingCompanyById(
        int id,
        [Service] publishRepo.publishRepo repo)
    {
        return await repo.GetPublishingCompanyById(id);
    }

    public async Task<List<Author>> GetAllAuthors(
        [Service] AuthorRepo  repo)
    {
        var allAuthors = await repo.GetAuthors();
        return allAuthors;
    }

        public async Task<Author?> GetAuthor(
        [Service] AuthorRepo  repo,int id)
    {
        var allAuthors = await repo.GetAuthorRepoById(id);
        return allAuthors;
    }

            public async Task<List<Book>> GetAllBooks(
        [Service] BookRepo  repo)
    {
        var allBooks = await repo.GetBooks();
        return allBooks;
    }

    public async Task<Book?> GetBookById(
        int id,
        [Service] BookRepo  repo)
    {
        return await repo.GetBookRepoById(id);
    }



    }
    
}
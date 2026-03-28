using Auhtors;
using AuthorsRepo;

namespace authorquery
{
    public class authorquery
    {
        public async Task<List<Author>> GetAllAuthors(
        [Service] AuthorRepo  repo)
    {
        var allAuthors = await repo.GetAuthors();
        return allAuthors;
    }

    public async Task<Author> GetAuthorById(
        int id,
        [Service] AuthorRepo  repo)
    {
        return await repo.GetAuthorRepoById(id);
    }
    }
    
}
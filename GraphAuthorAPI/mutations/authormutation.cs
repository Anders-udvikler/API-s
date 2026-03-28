using Auhtors;
using AuthorsRepo;

namespace authormutation
{
    public class authormutation
    {
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
    
        public async Task<Author> DeleteBook(int id,
        [Service] AuthorRepo repo)
        {
            return await repo.DeleteAuthor(id);
        }

}
}
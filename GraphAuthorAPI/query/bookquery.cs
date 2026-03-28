using Books;
using BooksRepo;

namespace bookquery
{
    public class bookquery
    {
        public async Task<List<Book>> GetAllBooks(
        [Service] BookRepo  repo)
    {
        var allBooks = await repo.GetBooks();
        return allBooks;
    }

    public async Task<Book> GetBookById(
        int id,
        [Service] BookRepo  repo)
    {
        return await repo.GetBookRepoById(id);
    }

    }
    
}
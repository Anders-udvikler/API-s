using BooksRepo;
using Books;
namespace bookmutation
{
    public class bookmutation
    {
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
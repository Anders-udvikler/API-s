using Auhtors;
using AuthorsRepo;
using Books;
using BooksRepo;
using Microsoft.Data.SqlClient;
using publishingcompanies;
using publishRepo;
namespace publishingmutation
{
    public class publishingmutation
    {
        public async Task<publishingcompany> AddPublishingCompany( publishingcompany publishingCompany,
        [Service] publishRepo.publishRepo repo)
    {
        try
        {
            publishingcompany company = await repo.AddPublishingCompany(publishingCompany);
            return company;
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
      public async Task<publishingcompany> UpdatePublishingCompany(
        int id, publishingcompany publishingcompany,
        [Service] publishRepo.publishRepo repo)
        {
            try
            {
                publishingcompany company= await repo.UpdatePublishingCompany(publishingcompany, id);
                return company;
            }
            catch(SqlException )
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {id} does not exist in the database");
                return null;
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    
        public async Task<publishingcompany> DeletePublishingCompany(int id,
        [Service] publishRepo.publishRepo repo)
        {
            try
            {
                publishingcompany company = await repo.DeletePublishingCompany(id);
                return company;
            }
            catch(SqlException )
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {id} does not exist in the database");
                return null;
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
            
        }

                public async Task<Author> AddAuthor(Author au,
        [Service] AuthorRepo repo)
    {
        Author author = await repo.AddAuthor(au);
        return author;
    }
      public async Task<Author> UpdateAuthor(
        int id, Author author,
        [Service] AuthorRepo repo)
        {
            try
            {
                Author updatedAuthor = await repo.UpdateAuthor(author, id);
                return updatedAuthor;
            }
            catch(SqlException)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {id} does not exist in the database");
                return null;
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
            
        }
    
        public async Task<Author> DeleteAuthor(int id,
        [Service] AuthorRepo repo)
        {
            try
            {
                Author author = await repo.DeleteAuthor(id);
                return author;
            }
            catch(SqlException)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {id} does not exist in the database");
                return null;
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
            
        }

                public async Task<Book> AddBook( Book book,
        [Service] BookRepo repo)
    {
        try
        {
            Book addedBook = await repo.AddBook(book);
            return addedBook;
        }
        catch(SqlException)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"An error occurred: {book} already exists in the database");
            return null;
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
      public async Task<Book> UpdateBook(
        int id, Book book,
        [Service] BookRepo repo)
        {
            try
            {
                Book updatedBook = await repo.UpdateBook(book, id);
                return updatedBook;
            }
            catch(SqlException)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {id} does not exist in the database");
                return null;
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    
        public async Task<Book> DeleteBook(int id,
        [Service] BookRepo repo)
        {
            try
            {
                Book deletedBook = await repo.DeleteBook(id);
                return deletedBook;
            }
            catch(SqlException sql)
            
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {id} does not exist in the database");
                return null;
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

}
}
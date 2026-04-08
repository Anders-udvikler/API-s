using Auhtors;
using AuthorsRepo;
using Books;
using BooksRepo;
using Microsoft.Data.SqlClient;
using publishingcompanies;
using publishRepo;

namespace query
{
    public class query
    {
        public async Task<List<publishingcompany?>?> GetAllPublishingCompanies(
        [Service] publishRepo.publishRepo repo)
    {
        try
        {
            var allPublishingCompanies = await repo.GetPublishingCompanies();
            return allPublishingCompanies;
                
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }

    public async Task<publishingcompany?> GetPublishingCompanyById(
        int id,
        [Service] publishRepo.publishRepo repo)
    {
        try
        {
            return await repo.GetPublishingCompanyById(id);
        }
        catch (SqlException ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }

    }

    public async Task<List<Author>> GetAllAuthors(
        [Service] AuthorRepo  repo)
    {
            try
            {
                var allAuthors = await repo.GetAuthors();
                return allAuthors;
            }
            catch (SqlException ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
    }

        public async Task<Author?> GetAuthor(
        [Service] AuthorRepo  repo,int id)
    {
        try
        {
            var allAuthors = await repo.GetAuthorRepoById(id);
            return allAuthors;
        }
        catch (SqlException ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }

            public async Task<List<Book?>?> GetAllBooks(
        [Service] BookRepo  repo)
    {
        try
        {
            var allBooks = await repo.GetBooks();
            return allBooks;
        }
        catch (SqlException ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }

    }

    public async Task<Book?> GetBookById(
        int id,
        [Service] BookRepo  repo)
    {
        try
        {
                        return await repo.GetBookRepoById(id);
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
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
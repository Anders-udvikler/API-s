using Auhtors;
using Books;
using Microsoft.Data.Sqlite;
using AuthorsRepo;
using publishRepo;
using Microsoft.Data.SqlClient;

namespace BooksRepo
{
    public class BookRepo
    {
        string querygetall = "select nBookID, cTitle,nAuthorID,nPublishingCompanyId, nPublishingYear from tbook";
        string querygetid = "select nBookID, cTitle, nAuthorID, nPublishingCompanyId, nPublishingYear from tbook where nBookID = @Id";
        string querygetAdd = "insert into tbook (nBookID, cTitle, nAuthorID, nPublishingCompanyId, nPublishingYear) values (@Id, @cTitle, @nAuthorID, @nPublishingCompanyId, @nPublishingYear)";
        string querygetDelete = "delete from tbook where nBookID = @Id";

        string querygetUpdate = "update tbook set cTitle = @cTitle, nAuthorID = @nAuthorID, nPublishingCompanyId = @nPublishingCompanyId, nPublishingYear = @nPublishingYear where nBookID = @Id";

        private readonly string _connectionString;

        private readonly publishRepo.publishRepo _publishRepo;
        private readonly AuthorRepo _authorRepo;

        public BookRepo(string connectionString)
        {
            _connectionString = connectionString;
            _publishRepo = new publishRepo.publishRepo(connectionString);
            _authorRepo = new AuthorRepo(connectionString);
        }

        public async Task<Book?> GetBookRepoById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(querygetid, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Book
                            {
                                Id = Convert.ToInt32(reader["nBookID"]),
                                Title = Convert.ToString(reader["cTitle"]),
                                AuthorId = Convert.ToInt32(reader["nAuthorID"]),
                                PublishingCompanyId = Convert.ToInt32(reader["nPublishingCompanyId"]),
                                Publishingyear = Convert.ToInt32(reader["nPublishingYear"])
                            };
                        }
                    }
                }
                return null;
            }
        }

        public async Task<Book> AddBook(Book book)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(querygetAdd, connection))
                {
                    var company = await _publishRepo.GetPublishingCompanyById(book.PublishingCompanyId);
                    var author = await _authorRepo.GetAuthorRepoById(book.AuthorId);
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@cTitle", book.Title);
                    command.Parameters.AddWithValue("@nAuthorID", author.Id);
                    command.Parameters.AddWithValue("@nPublishingCompanyId", company.Id);
                    command.Parameters.AddWithValue("@nPublishingYear", book.Publishingyear);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                            return new Book
                            {
                                Id = book.Id,
                                Title = book.Title,
                                AuthorId = author.Id,
                                PublishingCompanyId = company.Id,
                                Publishingyear = book.Publishingyear
                            };
                    }
                } 
            }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
                        catch (Exception ex)
            {
                Console.WriteLine($"Error adding book: {ex.Message}");
                throw;
            }
    }

        public async Task<Book> UpdateBook(Book book, int id)
        {
            try
            {
                 using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(querygetUpdate, connection))
                {
                    var company = await _publishRepo.GetPublishingCompanyById(book.PublishingCompanyId);
                    var author = await _authorRepo.GetAuthorRepoById(book.AuthorId);
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@cTitle", book.Title);
                    command.Parameters.AddWithValue("@nAuthorID", author.Id);
                    command.Parameters.AddWithValue("@nPublishingCompanyId", company.Id);
                    command.Parameters.AddWithValue("@nPublishingYear", book.Publishingyear);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                            return new Book
                            {
                                Id =book.Id,
                                Title = book.Title,
                                AuthorId =author.Id,
                                PublishingCompanyId = company.Id,
                                Publishingyear = book.Publishingyear
                            };
                    }
                }
            }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating book: {ex.Message}");
                throw;
            }
        }

        public async Task<Book> DeleteBook(int id)
        {
            try
            {
                 using (var connection = new SqliteConnection(_connectionString))
                 {
                    using (var command = new SqliteCommand(querygetDelete, connection))
                    {
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                            return new Book
                            {
                                Id = id
                            };
                    }
                }
           }
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error deleting book with id {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Book>> GetBooks()
        {
            try
            {
                            var books = new List<Book>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(querygetall, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            books.Add(new Book
                            {
                                Id = Convert.ToInt32(reader["nBookID"]),
                                Title = Convert.ToString(reader["cTitle"]),
                                AuthorId = Convert.ToInt32(reader["nAuthorID"]),
                                PublishingCompanyId = Convert.ToInt32(reader["nPublishingCompanyId"]),
                                Publishingyear = Convert.ToInt32(reader["nPublishingYear"])
                            });
                        }
                    }
                }
                return books;
            }
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving books: {ex.Message}");
                throw;
            }
        }
}
}
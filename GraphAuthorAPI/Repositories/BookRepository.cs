using Auhtors;
using Books;
using MySqlConnector;

namespace BooksRepo
{
    public class BookRepo
    {
        string querygetall = "select idbook, Name, AuthorId, PublishingCompanyId, Publishingyear from Book";
        string querygetid = "select idbook, Name, AuthorId, PublishingCompanyId, Publishingyear from Book where idbook = @Id";
        string querygetAdd = "insert into Book (idbook, Name, AuthorId, PublishingCompanyId, Publishingyear) values (@Id, @Name, @AuthorId, @PublishingCompanyId, @Publishingyear)";
        string querygetDelete = "delete from Book where idbook = @Id";

        string querygetUpdate = "update Book set Name = @Title, AuthorId = @AuthorId, PublishingCompanyId = @PublishingCompanyId, Publishingyear = @Publishingyear where idbook = @Id";

        private readonly string _connectionString;

        public BookRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Book?> GetBookRepoById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetid, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Book
                            {
                                Id = (int)reader["idbook"],
                                Title = reader["Name"].ToString(),
                                AuthorId = (int)reader["AuthorId"],
                                PublishingCompanyId = (int)reader["PublishingCompanyId"],
                                Publishingyear = (int)reader["Publishingyear"]
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
                using (var connection = new MySqlConnection(_connectionString))
                {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetAdd, connection))
                {
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@Name", book.Title);
                    command.Parameters.AddWithValue("@AuthorId", book.AuthorId);
                    command.Parameters.AddWithValue("@PublishingCompanyId", book.PublishingCompanyId);
                    command.Parameters.AddWithValue("@Publishingyear", book.Publishingyear);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                            return new Book
                            {
                                Id = book.Id,
                                Title = book.Title,
                                AuthorId = book.AuthorId,
                                PublishingCompanyId = book.PublishingCompanyId,
                                Publishingyear = book.Publishingyear
                            };
                    }
                } 
            }
               return null;
            }
            catch (MySqlException ex)
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
                 using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetUpdate, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@AuthorId", book.AuthorId);
                    command.Parameters.AddWithValue("@PublishingCompanyId", book.PublishingCompanyId);
                    command.Parameters.AddWithValue("@Publishingyear", book.Publishingyear);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                            return new Book
                            {
                                Id =book.Id,
                                Title = book.Title,
                                AuthorId = book.AuthorId,
                                PublishingCompanyId = book.PublishingCompanyId,
                                Publishingyear = book.Publishingyear
                            };
                    }
                }
            }
            }
            catch (MySqlException ex)
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
                 using (var connection = new MySqlConnection(_connectionString))
                 {
                    using (var command = new MySqlCommand(querygetDelete, connection))
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
            catch (MySqlException ex)
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
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetall, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            books.Add(new Book
                            {
                                Id = (int)reader["idbook"],
                                Title = reader["Name"].ToString(),
                                AuthorId = (int)reader["AuthorId"],
                                PublishingCompanyId = (int)reader["PublishingCompanyId"],
                                Publishingyear = (int)reader["Publishingyear"]
                            });
                        }
                    }
                }
                return books;
            }
                
            }
            catch (MySqlException ex)
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
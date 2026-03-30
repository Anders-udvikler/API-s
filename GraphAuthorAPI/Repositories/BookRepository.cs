using Auhtors;
using Books;
using MySqlConnector;

namespace BooksRepo
{
    public class BookRepo
    {
        string querygetall = "select idbook, Name, AuthorId, PublishingCompanyId, Publishingyear from Book";
        string querygetid = "select idbook, Name, AuthorId, PublishingCompanyId, Publishingyear from Book where idbook = @Id";
        string querygetAdd = "insert into Book (Id, Name, AuthorId, PublishingCompanyId, Publishingyear) values (@Id, @Name, @AuthorId, @PublishingCompanyId, @Publishingyear)";
        string querygetDelete = "delete from Book where Id = @Id";

        string querygetUpdate = "update Book set Name = @Name, AuthorId = @AuthorId, PublishingCompanyId = @PublishingCompanyId, Publishingyear = @Publishingyear where Id = @Id";

        private readonly string _connectionString;

        public BookRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Book> GetBookRepoById(int id)
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
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetAdd, connection))
                {
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@AuthorId", book.AuthorId);
                    command.Parameters.AddWithValue("@PublishingCompanyId", book.PublishingCompanyId);
                    command.Parameters.AddWithValue("@Publishingyear", book.Publishingyear);
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

        public async Task<Book> UpdateBook(Book book, int id)
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

        public async Task<Book> DeleteBook(int id)
        {

            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var command = new MySqlCommand(querygetDelete, connection))
                {
                    await connection.OpenAsync();
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
                    return null;
                }
           }
        }

        public async Task<List<Book>> GetBooks()
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
}
}
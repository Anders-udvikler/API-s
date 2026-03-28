using Auhtors;
using Books;
using Microsoft.Data.SqlClient;

namespace BooksRepo
{
    public class BookRepo
    {
        string querygetall = "select * from books";
        string querygetid = "select * from books where Id = @Id";
        string querygetAdd = "insert into books (Id, Title, AuthorId, PublishingCompanyId, Publishingyear) values (@Id, @Title, @AuthorId, @PublishingCompanyId, @Publishingyear)";
        string querygetDelete = "delete from books where Id = @Id";

        string querygetUpdate = "update books set Title = @Title, AuthorId = @AuthorId, PublishingCompanyId = @PublishingCompanyId, Publishingyear = @Publishingyear where Id = @Id";

        private readonly string _connectionString;

        public BookRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Book> GetBookRepoById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(querygetid, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Book
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
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

        public Book AddBook(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetAdd, connection))
                {
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@AuthorId", book.AuthorId);
                    command.Parameters.AddWithValue("@PublishingCompanyId", book.PublishingCompanyId);
                    command.Parameters.AddWithValue("@Publishingyear", book.Publishingyear);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Book
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
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

        public Book UpdateBook(Book book, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetUpdate, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@AuthorId", book.AuthorId);
                    command.Parameters.AddWithValue("@PublishingCompanyId", book.PublishingCompanyId);
                    command.Parameters.AddWithValue("@Publishingyear", book.Publishingyear);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Book
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
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

        public async Task DeleteAuthor(int id)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetid, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
           }
        }

        public async Task<List<Book>> GetBooks()
        {
            var books = new List<Book>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(querygetall, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            books.Add(new Book
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
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
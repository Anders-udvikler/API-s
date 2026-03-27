using Auhtors;
using Books;
using Microsoft.Data.SqlClient;

namespace BooksRepo
{
    public class BookRepo
    {
        string querygetall = "";
        string querygetid = "";
        string querygetAdd = "";
        string querygetDelete = "";

        string querygetUpdate = "";

        private readonly string _connectionString;

        public BookRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Book GetBookRepoById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetid, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Books
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Country = reader["Country"].ToString()
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
                    command.Parameters.AddWithValue("@Name", book.Name);
                    command.Parameters.AddWithValue("@Country", book.Country);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Book
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Country = reader["Country"].ToString()
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
                    command.Parameters.AddWithValue("@Name", book.Name);
                    command.Parameters.AddWithValue("@Country", book.Country);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Book
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Country = reader["Country"].ToString()
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

        public List<Book> GetBooksById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetid, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Author
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Country = reader["Country"].ToString()
                            };
                        }
                    }
                }
                return null;
            }
        }
}
}
using Auhtors;
using Microsoft.Data.SqlClient;

namespace AuthorsRepo
{
    public class AuthorRepo
    {
        string querygetall = "select * from authors";
        string querygetid = "select * from authors where Id = @Id";
        string querygetAdd = "insert into authors (Id, Name, Surname) values (@Id, @Name, @Surname)";
        string querygetDelete = "delete from authors where Id = @Id";
        string querygetUpdate = "update authors set Name = @Name, Surname = @Surname where Id = @Id";

        private readonly string _connectionString;

        public AuthorRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Author> GetAuthorRepoById(int id)
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
                            return new Author
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Surname = reader["Country"].ToString()
                            };
                        }
                    }
                }
                return null;
            }
        }

        public async Task<Author> AddAuthor(Author author)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(querygetAdd, connection))
                {
                    command.Parameters.AddWithValue("@Id", author.Id);
                    command.Parameters.AddWithValue("@Name", author.Name);
                    command.Parameters.AddWithValue("@Surname", author.Surname);
                    command.ExecuteNonQuery();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Author
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Surname = reader["Country"].ToString()
                            };
                        }
                    }
                }
                return null;
            
        }
    }

        public async Task<Author> UpdateAuthor(Author author, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(querygetUpdate, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", author.Name);
                    command.Parameters.AddWithValue("@Surname", author.Surname);
                    command.ExecuteNonQuery();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Author
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Surname = reader["Surname"].ToString()
                            };
                        }
                    }
                }
                return null;
        }
    }

        public async Task<Author> DeleteAuthor(int id)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(querygetid, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                    if (await command.ExecuteReaderAsync() != null)
                    {
                        return new Author()
                        {
                            Id = id
                        };
                    }
                }
                return null;
           }
        }

        public async Task<List<Author>> GetAuthors()
        {
            var authors = new List<Author>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetall, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            authors.Add(new Author
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Surname = reader["Country"].ToString()
                            });
                        }
                    }
                }
                return authors;
            }
        }
}
}
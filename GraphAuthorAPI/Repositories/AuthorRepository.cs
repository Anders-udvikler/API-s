using Auhtors;
using Microsoft.Data.SqlClient;
using MySqlConnector;

namespace AuthorsRepo
{
    public class AuthorRepo
    {
        string querygetall = "select Id, Name, Surname from Author";
        string querygetid = "select * from Author where Id = @Id";
        string querygetAdd = "insert into Author (Id, Name, Surname) values (@Id, @Name, @Surname)";
        string querygetDelete = "delete from Author where Id = @Id";
        string querygetUpdate = "update Author set Name = @Name, Surname = @Surname where Id = @Id";

        private readonly string _connectionString;

        public AuthorRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Author?> GetAuthorRepoById(int id)
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

        public async Task<Author?> AddAuthor(Author author)
        {
            try
            {
                            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetAdd, connection))
                {
                    command.Parameters.AddWithValue("@Id", author.Id);
                    command.Parameters.AddWithValue("@Name", author.Name);
                    command.Parameters.AddWithValue("@Surname", author.Surname);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                            return new Author
                            {
                                Id = author.Id,
                                Name = author.Name,
                                Surname = author.Surname
                            };
                    }
                }
            }
            return null;
            }
            catch (SqlException)
            {
                // Handle SQL exceptions (e.g., connection issues, query errors)
                Console.WriteLine("A database error occurred while adding the author.");
                throw; // Re-throw the exception after logging it
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
    }

        public async Task<Author?> UpdateAuthor(Author author, int id)
        {
            try
            {
                 using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetUpdate, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", author.Name);
                    command.Parameters.AddWithValue("@Surname", author.Surname);
                    using ( var reader = await command.ExecuteReaderAsync())
                    {
                            return new Author
                            {
                                Id =id,
                                Name =author.Name,
                                Surname = author.Surname
                            };
                    }
                }
                return null;
                
            }}
            catch(SqlException)
            {
                // Handle SQL exceptions (e.g., connection issues, query errors)
                Console.WriteLine("A database error occurred while updating the author.");
                throw; // Re-throw the exception after logging it
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
        }

        public async Task<Author?> DeleteAuthor(int id)
        {
            try
            { 
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetDelete, connection))
                {
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
            catch (SqlException)
            {
                // Handle SQL exceptions (e.g., connection issues, query errors)
                Console.WriteLine("A database error occurred while deleting the author.");
                throw; // Re-throw the exception after logging it
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
        }

        public async Task<List<Author>> GetAuthors()
        {
            try
            {
                var authors = new List<Author>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(querygetall, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            authors.Add(new Author
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Surname = reader["Surname"].ToString()
                            });
                        }
                    }
                }
                return authors;
            }
            }
            catch(SqlException)
            {
                // Handle SQL exceptions (e.g., connection issues, query errors)
                Console.WriteLine("A database error occurred while retrieving authors.");
                throw; // Re-throw the exception after logging it
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
        }
}
}
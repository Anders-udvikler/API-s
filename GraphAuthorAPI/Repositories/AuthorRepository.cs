using Auhtors;
using Microsoft.Data.SqlClient;

namespace AuthorsRepo
{
    public class AuthorRepo
    {
        string querygetall = "";
        string querygetid = "";
        string querygetAdd = "";
        string querygetDelete = "";

        string querygetUpdate = "";

        private readonly string _connectionString;

        public AuthorRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AuthorRepo GetAuthorRepoById(int id)
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
                                Surname = reader["Country"].ToString(),
                                PublishingCompanyId = Convert.ToInt32(reader["publishingcompany"]),
                                PublishingYear = Convert.ToInt32(reader["publishingyear"])
                            };
                        }
                    }
                }
                return null;
            }
        }

        public Author AddAuthor(Author author)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetAdd, connection))
                {
                    command.Parameters.AddWithValue("@Id", author.Id);
                    command.Parameters.AddWithValue("@Name", author.Name);
                    command.Parameters.AddWithValue("@Surname", author.Surname);
                    command.Parameters.AddWithValue("@PublishingCompanyId", author.PublishingCompanyId);
                    command.Parameters.AddWithValue("@Publishingyear", author.Publishingyear);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Author
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Surname = reader["Country"].ToString(),
                                PublishingCompanyId = Convert.ToInt32(reader["publishingcompany"]),
                                PublishingYear = Convert.ToInt32(reader["publishingyear"])
                            };
                        }
                    }
                }
                return null;
            
        }
    }

        public Author UpdateAuthor(Author author, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetUpdate, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", author.Name);
                    command.Parameters.AddWithValue("@Country", author.Country);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Author
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Surname = reader["Country"].ToString()
                                publishingcompanyid = reader["publishingcompany"].ToString()
                                publishingyear = reader["publishingyear"].ToString()     
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

        public List<Author> GetAuthorsById(int id)
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
                                Surname = reader["Country"].ToString(),
                                PublishingCompanyId = reader["publishingcompany"].ToString(),
                                PublishingYear = reader["publishingyear"].ToString()
                            };
                        }
                    }
                }
                return null;
            }
        }
}
}
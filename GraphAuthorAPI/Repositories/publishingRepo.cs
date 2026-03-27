using Microsoft.Data.SqlClient;
using publishingcompanies;

namespace publishRepo
{
    public class publishRepo
    {
        string querygetall = "select * from publishingcompany";
        string querygetid = "select * from publishingcompany where Id = @Id";
        string querygetAdd = "insert into publishingcompany (Id, Name, Country) values (@Id, @Name, @Country)";
        string querygetDelete = "delete from publishingcompany where Id = @Id";
        string querygetUpdate = "update publishingcompany set Name = @Name, Country = @Country where Id = @Id";

        private readonly string _connectionString;

        public publishRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public publishingcompany GetPublishingCompanyById(int id)
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
                            return new publishingcompany
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

        public publishingcompany AddPublishingCompany(publishingcompany company)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetAdd, connection))
                {
                    command.Parameters.AddWithValue("@Id", company.Id);
                    command.Parameters.AddWithValue("@Name", company.Name);
                    command.Parameters.AddWithValue("@Country", company.Country);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new publishingcompany
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

        public publishingcompany UpdatePublishingCompany(publishingcompany company, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetUpdate, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", company.Name);
                    command.Parameters.AddWithValue("@Country", company.Country);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new publishingcompany
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

        public async Task DeletePublishingCompany(int id)
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

        public List<publishingcompany> GetPublishingCompanies()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querygetall, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var companies = new List<publishingcompany>();
                        while (reader.Read())
                        {
                            companies.Add(new publishingcompany
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Country = reader["Country"].ToString()
                            });
                        }
                        return companies;
                    }
                }
                return null;
            }
        }
}
}
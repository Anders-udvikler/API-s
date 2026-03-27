using Microsoft.Data.SqlClient;
using publishingcompanies;

namespace publishRepo
{
    public class publishRepo
    {
        string querygetall = "";
        string querygetid = "";
        string querygetAdd = "";
        string querygetDelete = "";

        string querygetUpdate = "";

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

        public List<publishingcompany> GetPublishingCompanyById(int id)
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
}
}
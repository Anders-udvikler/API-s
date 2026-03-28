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

        public async Task<publishingcompany> GetPublishingCompanyById(int id)
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
                            return new publishingcompany
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString()
                            };
                        }
                    }
                }
                return null;
            }
        }

        public async Task<publishingcompany> AddPublishingCompany(publishingcompany company)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(querygetAdd, connection))
                {
                    command.Parameters.AddWithValue("@Id", company.Id);
                    command.Parameters.AddWithValue("@Name", company.Name);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new publishingcompany
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString()
                            };
                        }
                    }
                }
                return null;
            
        }
    }

        public async Task<publishingcompany> UpdatePublishingCompany(publishingcompany company, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(querygetUpdate, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", company.Name);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new publishingcompany
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString()
                            };
                        }
                    }
                }
                return null;
        }
    }

        public async Task<publishingcompany> DeletePublishingCompany(int id)
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
                }
                return null;
           }
        }

        public async Task<List<publishingcompany>> GetPublishingCompanies()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(querygetall, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var companies = new List<publishingcompany>();
                        while (await reader.ReadAsync())
                        {
                            companies.Add(new publishingcompany
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString()
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
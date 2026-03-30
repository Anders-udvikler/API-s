using MySqlConnector;
using publishingcompanies;

namespace publishRepo
{
    public class publishRepo
    {
        string querygetall = "select idpublishing,name from publishingcompanies";
        string querygetid = "select idpublishing,name from publishingcompanies where idpublishing = @Id";
        string querygetAdd = "insert into publishingcompanies (idpublishing, name) values (@Id, @Name)";
        string querygetDelete = "delete from publishingcompanies where idpublishing = @Id";
        string querygetUpdate = "update publishingcompanies set Name = @Name where idpublishing = @Id";

        private readonly string _connectionString;

        public publishRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<publishingcompany?> GetPublishingCompanyById(int id)
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
                            return new publishingcompany
                            {
                                Id = (int)reader["idpublishing"],
                                Name = reader["name"].ToString()
                            };
                        }
                    }
                }
                return null;
            }
        }

        public async Task<publishingcompany?> AddPublishingCompany(publishingcompany company)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetAdd, connection))
                {
                    command.Parameters.AddWithValue("@Id", company.Id);
                    command.Parameters.AddWithValue("@Name", company.Name);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new publishingcompany
                            {
                                Id = (int)reader["idpublishing"],
                                Name = reader["name"].ToString()
                            };
                        }
                    }
                }
                return null;
            
        }
    }

        public async Task<publishingcompany?> UpdatePublishingCompany(publishingcompany company, int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetUpdate, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", company.Name);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new publishingcompany
                            {
                                Id = (int)reader["idpublishing"],
                                Name = reader["name"].ToString()
                            };
                        }
                    }
                }
                return null;
        }
    }

        public async Task<publishingcompany?> DeletePublishingCompany(int id)
        {

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetDelete, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
                return null;
           }
        }

        public async Task<List<publishingcompany>> GetPublishingCompanies()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(querygetall, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var companies = new List<publishingcompany>();
                        while (await reader.ReadAsync())
                        {
                            companies.Add(new publishingcompany
                            {
                                Id = (int)reader["idpublishing"],
                                Name = reader["name"].ToString()
                            });
                        }
                        return companies;
                    }
                }
            }
        }
}
}
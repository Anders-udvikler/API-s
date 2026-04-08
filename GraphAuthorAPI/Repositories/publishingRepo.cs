using Microsoft.Data.SqlClient;
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
            try
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
                }
            }
            catch(SqlException)
            {
                // Handle SQL exceptions (e.g., connection issues, query errors)
                Console.WriteLine("A database error occurred while retrieving the publishing company.");
                throw; // Re-throw the exception after logging it
            }
             catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return null;
        }

        public async Task<publishingcompany?> AddPublishingCompany(publishingcompany company)
        {
            try
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
                                Id = company.Id,
                                Name = company.Name
                            };
                        }
                    }
                    return null;
                }}
            }
            catch (SqlException)
            {
                // Handle SQL exceptions (e.g., connection issues, query errors)
                Console.WriteLine("A database error occurred while adding the publishing company.");
                throw; // Re-throw the exception after logging it
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
            
    }

        public async Task<publishingcompany?> UpdatePublishingCompany(publishingcompany company, int id)
        {
            try
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
                            return new publishingcompany
                            {
                                Id = company.Id,
                                Name = company.Name
                            };
                    }
                }
        }
                
            }
            catch (SqlException)
            {
                // Handle SQL exceptions (e.g., connection issues, query errors)
                Console.WriteLine("A database error occurred while updating the publishing company.");
                throw; // Re-throw the exception after logging it
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
        }

        public async Task<publishingcompany?> DeletePublishingCompany(int id)
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
                }
                return new publishingcompany
                {
                    Id = id 
                };
            }}
            catch (SqlException)
            {
                // Handle SQL exceptions (e.g., connection issues, query errors)
                Console.WriteLine("A database error occurred while deleting the publishing company.");
                throw; // Re-throw the exception after logging it
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
        }

        public async Task<List<publishingcompany>> GetPublishingCompanies()
        {
            try
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
            }}
            catch (SqlException)
            {
                // Handle SQL exceptions (e.g., connection issues, query errors)
                Console.WriteLine("A database error occurred while retrieving the publishing companies.");
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
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;    
using publishingcompanies;

namespace publishRepo
{
    public class publishRepo
    {
        string querygetall = "select nPublishingCompanyID, cName from tpublishingcompany";
        string querygetid = "select nPublishingCompanyID, cName from tpublishingcompany where nPublishingCompanyID = @Id";
        string querygetAdd = "insert into tpublishingcompany (nPublishingCompanyID, cName) values (@Id, @Name)";
        string querygetDelete = "delete from tpublishingcompany where nPublishingCompanyID = @Id";
        string querygetUpdate = "update tpublishingcompany set cName = @Name where nPublishingCompanyID = @Id";

        private readonly string _connectionString;

        public publishRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<publishingcompany?> GetPublishingCompanyById(int id)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqliteCommand(querygetid, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new publishingcompany
                                {
                                    Id = Convert.ToInt32(reader["nPublishingCompanyID"]),
                                    Name = Convert.ToString(reader["cName"])
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
                using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(querygetAdd, connection))
                {
                    command.Parameters.AddWithValue("@Id", company.Id);
                    command.Parameters.AddWithValue("@Name", company.Name);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                            return new publishingcompany
                            {
                                Id = company.Id,
                                Name = company.Name
                            };
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
                            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(querygetUpdate, connection))
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
                            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(querygetDelete, connection))
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
                            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(querygetall, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var companies = new List<publishingcompany>();
                        while (await reader.ReadAsync())
                        {
                            companies.Add(new publishingcompany
                            {
                                Id = Convert.ToInt32(reader["nPublishingCompanyID"]),
                                Name = Convert.ToString(reader["cName"])
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
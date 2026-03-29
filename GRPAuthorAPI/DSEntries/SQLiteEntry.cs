using Microsoft.Data.Sqlite;

namespace GRPAuthorAPI.DSEntries;

public class SqLiteEntry
{
    public static List<BookDto>? AccessDs(String sqlQuery, string? bookTitle)
    {
        using var connection = new SqliteConnection("Data Source=DataSources/library.db");
        
        try
        {
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;

            if (sqlQuery.ToLower().Contains("insert") && bookTitle != null)
            {
                using var updater = command.ExecuteNonQueryAsync();
                command.CommandText =  String.Format("select * from Tbook where cTitle = '{0}';", bookTitle);
            }

            using var reader = command.ExecuteReader();

            List<BookDto> result = new List<BookDto>();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    BookDto bookDto = new BookDto();
                    bookDto.nBookId = reader.GetInt32(0);
                    bookDto.cTitle = reader.GetString(1);
                    bookDto.nAuthorID = reader.GetInt32(2);
                    bookDto.nPublicationYear = reader.GetInt32(3);
                    bookDto.nPublishingCompanyID = reader.GetInt32(4);
                    result.Add(bookDto);
                }
            }
            Console.WriteLine(result.Count);
            //Console.WriteLine(result[0]);
            return result;
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
        finally
        {
            connection.Close();
        }
    }
    
    public class BookDto
    {
        public int nBookId { get; set; }
        public string cTitle { get; set; }
        public int nAuthorID { get; set; }
        public int nPublishingCompanyID { get; set; }
        public int nPublicationYear { get; set; }
    }
}
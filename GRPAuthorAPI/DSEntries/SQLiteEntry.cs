using Microsoft.Data.Sqlite;

namespace GRPAuthorAPI.DSEntries;

public class SqLiteEntry
{
    public static List<BookDto> AccessDs(String sqlQuery, string? bookTitle, int? authorId)
    {
        using var connection = new SqliteConnection("Data Source=DataSources/library.db");
        try
        {
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;

            if (sqlQuery.ToLower().Contains("insert") && bookTitle != null && authorId != null)
            {
                using var updater = command.ExecuteNonQueryAsync();
                command.CommandText =  String.Format("select * from Tbook where cTitle = '{0}' " +
                                                     "and NAuthorId = {1};", 
                    bookTitle, authorId);
            }

            using var reader = command.ExecuteReader();

            List<BookDto> result = new List<BookDto>();
            while (reader.Read())
            {
                if (reader.IsDBNull(0))
                {
                    throw new NullReferenceException("Book not found");
                }
                BookDto bookDto = new BookDto();
                bookDto.NBookId = reader.GetInt32(0);
                bookDto.CTitle = reader.GetString(1);
                bookDto.NAuthorId = reader.GetInt32(2);
                bookDto.NPublicationYear = reader.GetInt32(3);
                bookDto.NPublishingCompanyId = reader.GetInt32(4);
                result.Add(bookDto);
            }
            return result;
        }
        catch (SqliteException ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }
    
    public class BookDto
    {
        public int NBookId { get; set; }
        public string CTitle { get; set; }
        public int NAuthorId { get; set; }
        public int NPublishingCompanyId { get; set; }
        public int NPublicationYear { get; set; }
    }
}
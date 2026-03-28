using GRPAuthorAPI.DSEntries;
using Grpc.Core;
using GrpcBooks;

namespace GRPAuthorAPI.Logic;

public class BookService(ILogger<BookService> logger) : GrpcBooks.BookService.BookServiceBase
{
    private readonly ILogger<BookService> _logger = logger;

    public override Task<GrpcBooks.Book> GetBookById(
        GrpcBooks.GetBookByIdRequest request,
        ServerCallContext context) //Todo what is this?
    {
        String sqlQuery = String.Format("Select * From Tbook Where nBookId = {0};", request.BookId);
        var result = SqLiteEntry.AccessDs(sqlQuery, null);
        SqLiteEntry.BookDto bookResult = result[0];
        
        return Task.FromResult(new GrpcBooks.Book
        {
            BookId = bookResult.nBookId,
            Title = bookResult.cTitle,
            AuthorId = bookResult.nAuthorID,
            PublisherId = bookResult.nPublishingCompanyID,
            PublicationYear = bookResult.nPublicationYear
        });
    }

    public override Task<GrpcBooks.CreateBookResponse> CreateBook(
        GrpcBooks.CreateBookRequest request,
        ServerCallContext context)
    {
        String sqlQueryAddBook = String.Format("INSERT INTO Tbook (cTitle, nAuthorID, nPublishingYear, nPublishingCompanyID) " +
                                               "VALUES ('{0}', {1}, {2}, {3});", 
            request.Title, request.AuthorId, request.PublicationYear, request.PublisherId);
        
        var result = SqLiteEntry.AccessDs(sqlQueryAddBook, request.Title);
        SqLiteEntry.BookDto bookResult = result[0];
        return Task.FromResult(new GrpcBooks.CreateBookResponse
        {
            BookId = bookResult.nBookId,
            Status = (GrpcBooks.Status) context.Status.StatusCode
        });
    }

    public override async Task WatchBooks(
        WatchBooksRequest request,
        IServerStreamWriter<GrpcBooks.Book> responseStream,
        ServerCallContext context)
    {
        var notifier = new BookNotifier();
        var subscriptionId = notifier.Subscribe(responseStream);

        //TODO remeber logic!
        try
        {
            // Keep connection alive until client disconnects
            await Task.Delay(Timeout.Infinite, context.CancellationToken);
        }
        catch (TaskCanceledException)
        {
            // client disconnected
        }
        finally
        {
            notifier.Unsubscribe(subscriptionId);
        }
    }
}


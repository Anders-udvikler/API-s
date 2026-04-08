using GRPAuthorAPI.DSEntries;
using Grpc.Core;
using GrpcBooks;
using Status = Grpc.Core.Status;

namespace GRPAuthorAPI.Logic;

public class BookService : GrpcBooks.BookService.BookServiceBase
{
    private readonly ILogger<BookService> _logger;
    private readonly BookNotifier _notifier;

    public BookService(ILogger<BookService> logger, BookNotifier notifier)
    {
        _logger = logger;
        _notifier = notifier;
    }
    public override async Task<Book> GetBookById(
        GetBookByIdRequest request,
        ServerCallContext context) //Todo what is this?
    {
        String sqlQuery = String.Format("Select * From Tbook Where nBookId = {0};", request.BookId);
        var result = SqLiteEntry.AccessDs(sqlQuery, null, null);

        if (result == null || result.Count == 0)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Book with id {request.BookId} was not found")
            );
        }
        
        SqLiteEntry.BookDto bookResult = result[0];
        
        return new Book
        {
            BookId = bookResult.NBookId,
            Title = bookResult.CTitle,
            AuthorId = bookResult.NAuthorId,
            PublisherId = bookResult.NPublishingCompanyId,
            PublicationYear = bookResult.NPublicationYear
        };
    }

    public override async Task<CreateBookResponse> CreateBook(
        CreateBookRequest request,
        ServerCallContext context)
    {
        
        String sqlQueryAddBook = String.Format("INSERT INTO Tbook (cTitle, nAuthorID, nPublishingYear, nPublishingCompanyID) " +
                                               "VALUES ('{0}', {1}, {2}, {3});", 
            request.Title, request.AuthorId, request.PublicationYear, request.PublisherId);
        
        var result = SqLiteEntry.AccessDs(sqlQueryAddBook, request.Title, request.AuthorId);
        
        if (result == null || result.Count == 0)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, "Book not found")
            );
        }
        
        var bookResult = result[0];
        var book = new Book
        {
            BookId = bookResult.NBookId,
            Title = bookResult.CTitle,
            AuthorId = bookResult.NAuthorId,
            PublisherId = bookResult.NPublishingCompanyId,
            PublicationYear = bookResult.NPublicationYear
        };

        try
        {
            await _notifier.BroadcastAsync(book);
        }
        catch
        {
            _logger.LogError($"Book {bookResult.NBookId} could not be broadcasted");         
        }

        return new CreateBookResponse
        {
            BookId = book.BookId,
            Status = (GrpcBooks.Status)context.Status.StatusCode
        };
    }

    public override async Task WatchBooks(
        WatchBooksRequest request,
        IServerStreamWriter<Book> responseStream,
        ServerCallContext context)
    {
        // stay connected to notifiier
        var subscriptionId = _notifier.Subscribe(responseStream);

        try
        {
            await Task.Delay(Timeout.Infinite, context.CancellationToken);
        }
        catch (TaskCanceledException)
        {
            _logger.LogInformation("Subscriber cancelled subscription: " + subscriptionId);
        }
        finally
        {
            try
            {
                _notifier.Unsubscribe(subscriptionId);    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}


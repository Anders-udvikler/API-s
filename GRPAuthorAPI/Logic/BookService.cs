using GRPAuthorAPI.DSEntries;
using Grpc.Core;
using GrpcBooks;

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
    public override Task<GrpcBooks.Book> GetBookById(
        GrpcBooks.GetBookByIdRequest request,
        ServerCallContext context) //Todo what is this?
    {
        String sqlQuery = String.Format("Select * From Tbook Where nBookId = {0};", request.BookId);
        var result = SqLiteEntry.AccessDs(sqlQuery, null);
        if (result == null || result.Count == 0)
        {
            throw new RpcException(
                new Grpc.Core.Status(Grpc.Core.StatusCode.NotFound, "Book not found")
            );
        }
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

    public override async Task<GrpcBooks.CreateBookResponse> CreateBook(
        GrpcBooks.CreateBookRequest request,
        ServerCallContext context)
    {
        
        String sqlQueryAddBook = String.Format("INSERT INTO Tbook (cTitle, nAuthorID, nPublishingYear, nPublishingCompanyID) " +
                                               "VALUES ('{0}', {1}, {2}, {3});", 
            request.Title, request.AuthorId, request.PublicationYear, request.PublisherId);
        
        var result = SqLiteEntry.AccessDs(sqlQueryAddBook, request.Title);
        
        if (result == null || result.Count == 0)
        {
            throw new RpcException(
                new Grpc.Core.Status(Grpc.Core.StatusCode.NotFound, "Book not found")
            );
        }

        //SqLiteEntry.BookDto bookResult = result[0];
        var bookResult = result[0];
        var book = new GrpcBooks.Book
        {
            BookId = bookResult.nBookId,
            Title = bookResult.cTitle,
            AuthorId = bookResult.nAuthorID,
            PublisherId = bookResult.nPublishingCompanyID,
            PublicationYear = bookResult.nPublicationYear
        };

        await _notifier.BroadcastAsync(book);

        /*return Task.FromResult(new GrpcBooks.CreateBookResponse
        {
            BookId = bookResult.nBookId,
            Status = (GrpcBooks.Status) context.Status.StatusCode
        });*/

        return new CreateBookResponse
        {
            BookId = book.BookId,
            Status = (GrpcBooks.Status)context.Status.StatusCode


        };
    }

    public override async Task WatchBooks(
        WatchBooksRequest request,
        IServerStreamWriter<GrpcBooks.Book> responseStream,
        ServerCallContext context)
    {
        //var notifier = new BookNotifier();
        //var subscriptionId = notifier.Subscribe(responseStream);
        // stay connect to notifiier
        var subscriptionId = _notifier.Subscribe(responseStream);

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
            //notifier.Unsubscribe(subscriptionId);
            _notifier.Unsubscribe(subscriptionId);
        }
    }
}


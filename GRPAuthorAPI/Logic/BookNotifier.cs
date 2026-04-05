using System.Collections.Concurrent;
using Grpc.Core;
using GrpcBooks;

namespace GRPAuthorAPI.Logic;

public class BookNotifier
{
    private readonly ConcurrentDictionary<Guid, IServerStreamWriter<Book>> _subscribers = new();

    public Guid Subscribe(IServerStreamWriter<Book> stream)
    {
        var id = Guid.NewGuid();
        _subscribers[id] = stream;
        return id;
    }

    public void Unsubscribe(Guid id)
    {
        try
        {
            var success = _subscribers.TryRemove(id, out _);
            if (!success)
            {
                throw new Exception($"{id} could not be removed");
            }
        }
        catch (ArgumentNullException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task BroadcastAsync(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);
        foreach (var subscriber in _subscribers.Values)
        {
            try
            {
                await subscriber.WriteAsync(book);
            }
            catch  (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
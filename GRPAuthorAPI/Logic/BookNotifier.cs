using System.Collections.Concurrent;
using Grpc.Core;
using GrpcBooks;

namespace GRPAuthorAPI.Logic;

public class BookNotifier
{
    private readonly ConcurrentDictionary<Guid, IServerStreamWriter<GrpcBooks.Book>> _subscribers = new();

    public Guid Subscribe(IServerStreamWriter<GrpcBooks.Book> stream)
    {
        var id = Guid.NewGuid();
        _subscribers[id] = stream;
        return id;
    }

    public void Unsubscribe(Guid id)
    {
        _subscribers.TryRemove(id, out _);
    }

    public async Task BroadcastAsync(GrpcBooks.Book book)
    {
        foreach (var subscriber in _subscribers.Values)
        {
            try
            {
                await subscriber.WriteAsync(book);
            }
            catch
            {
                // client probably disconnected, ignore or clean up later
            }
        }
    }
}
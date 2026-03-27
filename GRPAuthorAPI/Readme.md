#import:
##Server:
```
dotnet add package MagicOnion.Server
dotnet add package MagicOnion.Hosting
```
##Client:
```
dotnet add package MagicOnion.Client
dotnet add package Grpc.Net.Client
```
#Develop a gRPC service with the following RPCs:

#Unary: GetBookById(GetBookByIdRequest) returns (Book)
	**Parameter: book ID
#Unary: CreateBook(CreateBookRequest) returns (CreateBookResponse)
	**Parameters: book name, author ID, publisher ID, and publication year
#Streaming: WatchBooks(WatchBooksRequest) returns (stream Book)
	**Clients subscribed to WatchBooks will receive a message every time any other client adds a new book.
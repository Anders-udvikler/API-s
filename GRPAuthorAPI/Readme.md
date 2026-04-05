# Develop a gRPC service with the following RPCs:

Unary: **GetBookById(GetBookByIdRequest)** returns **(Book)**
* Parameter: book ID

Unary: **CreateBook(CreateBookRequest)** returns **(CreateBookResponse)**
* Parameters: book name, author ID, publisher ID, and publication year

Streaming: **WatchBooks(WatchBooksRequest)** returns **(stream Book)**

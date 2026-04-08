# API Testing (CLI)

This project exposes a **gRPC API** (not REST).
Use `grpcurl` from the command line to test each endpoint, similar to Postman.

## 1) Prerequisites

1. Install `grpcurl`:
   - Windows (winget):
     ```powershell
     winget install fullstorydev.grpcurl
     ```
   - Verify install:
     ```powershell
     grpcurl --version
     ```
   - If `grpcurl` is still not recognized, run with full path:
     ```powershell
     & "C:\Users\Admin\AppData\Local\Microsoft\WinGet\Packages\fullstorydev.grpcurl_Microsoft.Winget.Source_8wekyb3d8bbwe\grpcurl.exe" --version
     ```
   - If PowerShell finds it but Git Bash does not, restart Git Bash and test again.
   - If still missing in Git Bash, run this once to add common Windows App paths:
     ```bash
     export PATH="$PATH:/c/Users/$USERNAME/AppData/Local/Microsoft/WinGet/Links:/c/Users/$USERNAME/AppData/Local/Programs"
     grpcurl --version
     ```
2. Start the API from the project root:
   ```powershell
   dotnet run --launch-profile https
   ```

Default local URLs from `launchSettings.json`:

- HTTP: `http://localhost:5165`
- HTTPS: `https://localhost:7034`

All examples below use HTTPS on port 7034 with `-insecure` for local testing.

Note: gRPC requires HTTP/2. If your app is running only on `http://localhost:5165`, calls may time out with `context deadline exceeded` because that endpoint may serve HTTP/1.1 only.

## 2) Service and Methods

Proto package: `books`
Service: `BookService`

Full gRPC method names:

- `books.BookService/GetBookById`
- `books.BookService/CreateBook`
- `books.BookService/WatchBooks`

## 3) GetBookById (Unary)

Request type: `GetBookByIdRequest`

```powershell
grpcurl -insecure -import-path Contracts -proto books.proto -d '{"bookId": 1}' localhost:7034 books.BookService/GetBookById
```

Expected: One `Book` JSON object.

## 4) CreateBook (Unary)

Request type: `CreateBookRequest`

```powershell
grpcurl -insecure -import-path Contracts -proto books.proto -d '{"title":"CLI Test Book","authorId":12,"publisherId":3,"publicationYear":2024}' localhost:7034 books.BookService/CreateBook
```

Expected: `CreateBookResponse` with a generated `bookId` and a `status` value.

## 5) WatchBooks (Server Streaming)

Request type: `WatchBooksRequest` (empty object)

Use **two terminals**:

1. Terminal A: start stream listener

   ```powershell
   grpcurl -insecure -import-path Contracts -proto books.proto -d '{}' localhost:7034 books.BookService/WatchBooks
   ```

2. Terminal B: create a new book to trigger stream output
   ```powershell
   grpcurl -insecure -import-path Contracts -proto books.proto -d '{"title":"Stream Trigger Book","authorId":77,"publisherId":5,"publicationYear":2025}' localhost:7034 books.BookService/CreateBook
   ```

Expected: Terminal A receives the created `Book` object as streamed output.

## 6) Optional HTTPS Commands

If you run on HTTPS (`https://localhost:7034`), use `-insecure`:

```powershell
grpcurl -insecure -import-path Contracts -proto books.proto -d '{"bookId": 1}' localhost:7034 books.BookService/GetBookById
```

## 7) Quick Troubleshooting

- `Failed to dial target host`: Ensure API is running (`dotnet run`) and port matches.
- `grpcurl: command not found`: reopen terminal after install, or run the API tests from PowerShell.
- `grpcurl` still not recognized in PowerShell: run using full executable path shown in Prerequisites.
- `context deadline exceeded` on `localhost:5165`: start with `dotnet run --launch-profile https` and call `localhost:7034` with `-insecure`.
- `Symbol not found`: Ensure both flags are present:
  - `-import-path Contracts`
  - `-proto books.proto`
- Empty/Not found behavior for IDs: verify there is data in `DataSources/library.db`.

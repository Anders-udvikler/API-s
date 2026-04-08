# Develop a gRPC service with the following RPCs:

Unary: **GetBookById(GetBookByIdRequest)** returns **(Book)**
* Parameter: book ID

Unary: **CreateBook(CreateBookRequest)** returns **(CreateBookResponse)**
* Parameters: book name, author ID, publisher ID, and publication year

Streaming: **WatchBooks(WatchBooksRequest)** returns **(stream Book)**


postman:
https://team-ultra-super-mega-go.postman.co/workspace/6cff799c-f355-4c01-a179-382dffbafeb6


# 📚 gRPC Book Service

## Overview

This project implements a **gRPC-based Book Service** with:

* Unary RPC:

  * `GetBookById`
  * `CreateBook`
* Server Streaming RPC:

  * `WatchBooks`

The `WatchBooks` stream allows clients to receive real-time updates whenever a new book is created.

---

## 🧱 Technologies Used

* .NET (ASP.NET Core gRPC)
* SQLite
* Postman (for testing)
* grpcurl (optional CLI testing)

---

## ⚙️ Setup

### 1. Run the Service

Make sure the API is running:

```bash
cd GRPAuthorAPI
dotnet run
```

Default endpoint (example):

```
https://localhost:7143
```

---

### 2. Important Configuration

Ensure `Program.cs` contains:

```csharp
builder.Services.AddGrpc();
builder.Services.AddSingleton<BookNotifier>();
```

This is critical for streaming to work.

---

## 📡 gRPC Methods

### 🔹 GetBookById

Returns a single book.

**Request:**

```json
{
  "bookId": 1
}
```

---

### 🔹 CreateBook

Creates a new book and broadcasts it to subscribers.

**Request:**

```json
{
  "title": "Test Book",
  "authorId": 1,
  "publisherId": 1,
  "publicationYear": 2024
}
```

**Response:**

```json
{
  "bookId": 2009,
  "status": "OK"
}
```

---

### 🔹 WatchBooks (Streaming)

Subscribes to real-time updates.

**Request:**

```json
{}
```

---

## 🧪 Testing with Postman

### ⚠️ Important Order

Streaming only works if the client is subscribed **before** a book is created.

---

### ✅ Step 1: Subscribe to WatchBooks

1. Open Postman
2. Create a **gRPC request**
3. Enter:

   ```
   localhost:7143
   ```
4. Select:

   ```
   BookService → WatchBooks
   ```
5. Send request with:

```json
{}
```

6. Leave this request **running**

---

### ✅ Step 2: Create a Book

In a new tab:

1. Select:

   ```
   BookService → CreateBook
   ```
2. Send:

```json
{
  "title": "Live Test Book",
  "authorId": 1,
  "publisherId": 1,
  "publicationYear": 2024
}
```

---

### ✅ Expected Result

* `CreateBook` returns a response
* `WatchBooks` stream receives a new message:

```json
{
  "bookId": 2010,
  "title": "Live Test Book",
  "authorId": 1,
  "publisherId": 1,
  "publicationYear": 2024
}
```

---

## 🧪 Alternative Testing (grpcurl)

### Start stream:

```bash
grpcurl -insecure localhost:7143 BookService/WatchBooks
```

### Create book:

```bash
grpcurl -insecure -d '{
  "title":"CLI Test",
  "authorId":1,
  "publisherId":1,
  "publicationYear":2024
}' localhost:7143 BookService/CreateBook
```

---

## 🐛 Troubleshooting

### No data in WatchBooks

* Ensure `WatchBooks` is started BEFORE `CreateBook`
* Verify `BookNotifier` is registered as singleton
* Check logs for:

  ```
  Subscriber added. Count: 1
  Broadcasting to 1 subscribers
  ```

---

### Build error (file locked)

Stop the running API before rebuilding:

* Visual Studio → Stop button
* OR:

```bash
taskkill /IM GRPAuthorAPI.exe /F
```

---

### Status enum errors

Use:

```csharp
Status = GrpcBooks.Status.Ok
```

(Exact name depends on generated code)

---

## 🧠 Notes

* Streaming does **not** replay old messages
* Clients must be connected at the time of event
* `BookNotifier` manages active subscribers

---

## ✅ Summary

* Unary calls work like REST
* Streaming requires active subscription
* Broadcast happens on `CreateBook`
* Singleton notifier enables real-time updates

---

## 🚀 Result

Clients connected to `WatchBooks` receive live updates whenever new books are created.

---


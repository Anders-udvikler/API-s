# SOAP API Documentation (Library Management)

This document combines the current project README content with extra usage and troubleshooting guidance.

---

## Architecture Overview

```text
SOAP Contract (DTOs only)
        ↓
Service (business logic)
        ↓
Mapper
        ↓
EF Entities (tbook, tauthor, tpublishingcompany)
        ↓
SQLite
```

---

## Prerequisites

- .NET SDK 9.x (project target: `net9.0`)
- Postman (for request testing)
- Local development HTTPS certificate trusted (recommended)

Trust HTTPS certificate:

```bash
dotnet dev-certs https --trust
```

---

## Quick Start

1. Restore packages:

   ```bash
   dotnet restore
   ```

1. Build project:

   ```bash
   dotnet build Library.SoapApi.csproj
   ```

1. Run API:

   ```bash
   dotnet run --project Library.SoapApi.csproj
   ```

   - Or HTTPS profile:

   ```bash
   dotnet run --launch-profile https --project Library.SoapApi.csproj
   ```

1. SOAP endpoint:

   ```text
   https://localhost:7133/soap
   ```

Note:

- Launch profile URLs are in `Properties/launchSettings.json`.
- The service is configured at `/soap` in `Program.cs`.

---

## Postman

### Import Collection into Postman

1. Open Postman.
2. Click **Import** in the top-left corner.
3. Choose **Upload Files**.
4. Select `postman/Library SOAP API.postman_collection_updated.json`.
5. Click **Import**.
6. Use folders: **Book**, **Author**, **PublishingCompany**.

Tip:

- If your host/port differs, update request URL from `https://localhost:7133/soap`.

### Manual Setup in Postman

Endpoint:

```text
POST https://localhost:7133/soap
```

Headers:

```text
Content-Type: text/xml; charset=utf-8
SOAPAction: "http://library.soapapi.org/ILibrarySoapService/<<MethodName>>"
```

SOAP Envelope Template:

```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/"
                  xmlns:lib="http://library.soapapi.org/"
                  xmlns:mod="http://schemas.datacontract.org/2004/07/Library.SoapApi.Models">
   <soapenv:Header/>
   <soapenv:Body>
      <!-- METHOD HERE -->
   </soapenv:Body>
</soapenv:Envelope>
```

Important:

- For DTO-based operations (`UpdateBook`, `UpdateAuthor`, `UpdatePublishingCompany`), use the `mod` namespace for DTO fields.
- For `UpdateBook`, keep DTO field order as: `AuthorId`, `Id`, `PublishingCompanyId`, `PublishingYear`, `Title`.
- Sending fields out of order can deserialize missing values as defaults (for example `0`/`null`).

### Suggested Request Order

Use this order on a clean database to avoid reference issues:

1. `CreateAuthor`
2. `CreatePublishingCompany`
3. `CreateBook`

---

## SOAP Contract Summary

Service namespace:

```text
http://library.soapapi.org/
```

Contract source: `Contracts/ILibrarySoapService.cs`

| Group             | Operation                  | Parameters                                                                | Returns                      | Faults                             |
| ----------------- | -------------------------- | ------------------------------------------------------------------------- | ---------------------------- | ---------------------------------- |
| Book              | `CreateBook`               | `string title, int authorId, int publishingCompanyId, int publishingYear` | `int`                        | `ValidationFault`                  |
| Book              | `GetBookById`              | `int id`                                                                  | `BookDto`                    | `NotFoundFault`                    |
| Book              | `ListBooks`                | none                                                                      | `List<BookDto>`              | none                               |
| Book              | `UpdateBook`               | `BookDto book`                                                            | `bool`                       | `ValidationFault`, `NotFoundFault` |
| Book              | `DeleteBook`               | `int id`                                                                  | `bool`                       | `NotFoundFault`                    |
| Author            | `CreateAuthor`             | `string name, string surname`                                             | `int`                        | `ValidationFault`                  |
| Author            | `GetAuthorById`            | `int id`                                                                  | `AuthorDto`                  | `NotFoundFault`                    |
| Author            | `ListAuthors`              | none                                                                      | `List<AuthorDto>`            | none                               |
| Author            | `UpdateAuthor`             | `AuthorDto author`                                                        | `bool`                       | `ValidationFault`, `NotFoundFault` |
| Author            | `DeleteAuthor`             | `int id`                                                                  | `bool`                       | `NotFoundFault`, `ConflictFault`   |
| PublishingCompany | `CreatePublishingCompany`  | `string name`                                                             | `int`                        | `ValidationFault`                  |
| PublishingCompany | `GetPublishingCompanyById` | `int id`                                                                  | `PublishingCompanyDto`       | `NotFoundFault`                    |
| PublishingCompany | `ListPublishingCompanies`  | none                                                                      | `List<PublishingCompanyDto>` | none                               |
| PublishingCompany | `UpdatePublishingCompany`  | `PublishingCompanyDto company`                                            | `bool`                       | `ValidationFault`, `NotFoundFault` |
| PublishingCompany | `DeletePublishingCompany`  | `int id`                                                                  | `bool`                       | `NotFoundFault`, `ConflictFault`   |

---

## Fault and Error Behavior

Fault contracts used by the service:

- `ValidationFault`: Request data is invalid (missing/invalid values)
- `NotFoundFault`: Requested entity does not exist
- `ConflictFault`: Delete/update blocked by dependency constraints

Fault models:

- `Faults/ValidationFault.cs`
- `Faults/NotFoundFault.cs`
- `Faults/ConflictFault.cs`

---

## Detailed Request Examples

### Book Requests

#### CreateBook

SOAPAction: `ILibrarySoapService/CreateBook`

```xml
<lib:CreateBook>
   <lib:title>Elefantii Mananca Fructele</lib:title>
   <lib:authorId>512</lib:authorId>
   <lib:publishingCompanyId>160</lib:publishingCompanyId>
   <lib:publishingYear>2033</lib:publishingYear>
</lib:CreateBook>
```

#### GetBookById

SOAPAction: `ILibrarySoapService/GetBookById`

```xml
<lib:GetBookById>
   <lib:id>2000</lib:id>
</lib:GetBookById>
```

#### ListBooks

SOAPAction: `ILibrarySoapService/ListBooks`

```xml
<lib:ListBooks/>
```

#### UpdateBook

SOAPAction: `ILibrarySoapService/UpdateBook`

```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/"
                  xmlns:lib="http://library.soapapi.org/"
                  xmlns:mod="http://schemas.datacontract.org/2004/07/Library.SoapApi.Models">
   <soapenv:Header/>
   <soapenv:Body>
      <lib:UpdateBook>
         <lib:book>
            <mod:AuthorId>512</mod:AuthorId>
            <mod:Id>2000</mod:Id>
            <mod:PublishingCompanyId>160</mod:PublishingCompanyId>
            <mod:PublishingYear>2034</mod:PublishingYear>
            <mod:Title>Elefantii Mananca Fructele si Legumele - UPDATED</mod:Title>
         </lib:book>
      </lib:UpdateBook>
   </soapenv:Body>
</soapenv:Envelope>
```

#### DeleteBook

SOAPAction: `ILibrarySoapService/DeleteBook`

```xml
<lib:DeleteBook>
   <lib:id>2001</lib:id>
</lib:DeleteBook>
```

### Author Requests

#### CreateAuthor

SOAPAction: `ILibrarySoapService/CreateAuthor`

```xml
<lib:CreateAuthor>
   <lib:name>Lise Este</lib:name>
   <lib:surname>Foame</lib:surname>
</lib:CreateAuthor>
```

#### GetAuthorById

SOAPAction: `ILibrarySoapService/GetAuthorById`

```xml
<lib:GetAuthorById>
   <lib:id>512</lib:id>
</lib:GetAuthorById>
```

#### ListAuthors

SOAPAction: `ILibrarySoapService/ListAuthors`

```xml
<lib:ListAuthors/>
```

#### UpdateAuthor

SOAPAction: `ILibrarySoapService/UpdateAuthor`

```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/"
                  xmlns:lib="http://library.soapapi.org/"
                  xmlns:mod="http://schemas.datacontract.org/2004/07/Library.SoapApi.Models">
   <soapenv:Header/>
   <soapenv:Body>
      <lib:UpdateAuthor>
         <lib:author>
            <mod:Id>512</mod:Id>
            <mod:Name>Anna Este</mod:Name>
            <mod:Surname>Foame</mod:Surname>
         </lib:author>
      </lib:UpdateAuthor>
   </soapenv:Body>
</soapenv:Envelope>
```

#### DeleteAuthor

SOAPAction: `ILibrarySoapService/DeleteAuthor`

```xml
<lib:DeleteAuthor>
   <lib:id>513</lib:id>
</lib:DeleteAuthor>
```

### PublishingCompany Requests

#### CreatePublishingCompany

SOAPAction: `ILibrarySoapService/CreatePublishingCompany`

```xml
<lib:CreatePublishingCompany>
   <lib:name>Lise's Publish firm</lib:name>
</lib:CreatePublishingCompany>
```

#### GetPublishingCompanyById

SOAPAction: `ILibrarySoapService/GetPublishingCompanyById`

```xml
<lib:GetPublishingCompanyById>
   <lib:id>160</lib:id>
</lib:GetPublishingCompanyById>
```

#### ListPublishingCompanies

SOAPAction: `ILibrarySoapService/ListPublishingCompanies`

```xml
<lib:ListPublishingCompanies/>
```

#### UpdatePublishingCompany

SOAPAction: `ILibrarySoapService/UpdatePublishingCompany`

```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/"
                  xmlns:lib="http://library.soapapi.org/"
                  xmlns:mod="http://schemas.datacontract.org/2004/07/Library.SoapApi.Models">
   <soapenv:Header/>
   <soapenv:Body>
      <lib:UpdatePublishingCompany>
         <lib:company>
            <mod:Id>160</mod:Id>
            <mod:Name>Flourish and Blotts</mod:Name>
         </lib:company>
      </lib:UpdatePublishingCompany>
   </soapenv:Body>
</soapenv:Envelope>
```

#### DeletePublishingCompany

SOAPAction: `ILibrarySoapService/DeletePublishingCompany`

```xml
<lib:DeletePublishingCompany>
   <lib:id>159</lib:id>
</lib:DeletePublishingCompany>
```

---

## Database and Migrations

Database file:

```text
Db/library.db
```

### Common Commands

Create migration:

```bash
dotnet ef migrations add MigrationName
```

Apply migrations:

```bash
dotnet ef database update
```

List migrations:

```bash
dotnet ef migrations list --project Library.SoapApi.csproj --startup-project Library.SoapApi.csproj
```

Rollback to baseline:

```bash
dotnet ef database update 20260329133035_InitialCreate
```

Rollback all:

> **Warning:** This can remove all tables/data from db file.


```bash
dotnet ef database update 0
```

### Existing Database Baseline (Keep Data)

If the DB already contains data/tables, do not run an initial create migration directly.

1. Back up `Db/library.db`.
1. Ensure `__EFMigrationsHistory` contains:

   ```text
   MigrationId: 20260329133035_InitialCreate
   ProductVersion: 9.0.1
   ```

1. Re-run migrations list and confirm it is not pending.

---

## Project Structure

```text
Contracts/      SOAP service contract
Services/       Business logic implementation
Db/             DbContext and EF entities
Mapping/        Entity <-> DTO mapping
Faults/         SOAP fault contracts
Models/         DTOs
Migrations/     EF migration files
postman/        Postman collection
```

---

## Troubleshooting

### 1) EF command throws IndexOutOfRangeException

Cause: malformed command such as `dotnet ef migrations list --project` with no value.

Use:

```bash
dotnet ef migrations list --project Library.SoapApi.csproj --startup-project Library.SoapApi.csproj
```

### 2) `dotnet ef ...` says Build failed

Cause: project compile errors block EF tooling.

Fix order:

```bash
dotnet build Library.SoapApi.csproj
dotnet ef migrations list --project Library.SoapApi.csproj --startup-project Library.SoapApi.csproj
```

### 3) HTTPS/Postman connection issues

- Confirm API is running.
- Use launch profile URL from `Properties/launchSettings.json`.
- Trust dev certificate with `dotnet dev-certs https --trust`.

### 4) Migration version warning (tools/runtime mismatch)

Align EF tool and packages to the same version.

---

<!--
## Known Limitations / Next Improvements

- No authentication/authorization layer.
- No pagination/filtering/search endpoints.
- Limited automated testing coverage.
- No CI pipeline included.

--- -->

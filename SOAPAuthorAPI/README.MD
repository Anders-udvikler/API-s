# SOAP API for Library Management System

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

## Postman

### Setup

**Endpoint:**
`POST https://localhost:7133/soap`

**Headers:**

```text
Content-Type: text/xml; charset=utf-8
SOAPAction: "http://library.soapapi.org/ILibrarySoapService/<<MethodName>>"
```

**SOAP Envelope Template:**
(All requests should follow this template)

```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope"
                  xmlns:lib="http://library.soapapi.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <!-- METHOD HERE -->
   </soapenv:Body>
</soapenv:Envelope>
```

---

### Book Requests

#### CreateBook

**SOAPAction:** `ILibrarySoapService/CreateBook`

```xml
<lib:CreateBook>
   <lib:title>Harry Potter and the Philosopher's Stone</lib:title>
   <lib:authorId>1</lib:authorId>
   <lib:publishingCompanyId>1</lib:publishingCompanyId>
   <lib:publishingYear>1997</lib:publishingYear>
</lib:CreateBook>
```

#### GetBookById

**SOAPAction:** `ILibrarySoapService/GetBookById`

```xml
<lib:GetBookById>
   <lib:id>1</lib:id>
</lib:GetBookById>
```

#### ListBooks

**SOAPAction:** `ILibrarySoapService/ListBooks`

```xml
<lib:ListBooks/>
```

#### UpdateBook

**SOAPAction:** `ILibrarySoapService/UpdateBook`

```xml
<lib:UpdateBook>
   <lib:nBookID>1</lib:nBookID>
   <lib:cTitle>Harry Potter and the Sorcerer's Stone</lib:cTitle>
   <lib:nAuthorID>1</lib:nAuthorID>
   <lib:nPublishingCompanyID>1</lib:nPublishingCompanyID>
   <lib:nPublishingYear>1998</lib:nPublishingYear>
</lib:UpdateBook>
```

#### DeleteBook

**SOAPAction:** `ILibrarySoapService/DeleteBook`

```xml
<lib:DeleteBook>
   <lib:id>1</lib:id>
</lib:DeleteBook>
```

---

### Author Requests

#### CreateAuthor

**SOAPAction:** `ILibrarySoapService/CreateAuthor`

```xml
<lib:CreateAuthor>
   <lib:name>J.K.</lib:name>
   <lib:surname>Rowling</lib:surname>
</lib:CreateAuthor>
```

#### GetAuthorById

**SOAPAction:** `ILibrarySoapService/GetAuthorById`

```xml
<lib:GetAuthorById>
   <lib:id>1</lib:id>
</lib:GetAuthorById>
```

#### ListAuthors

**SOAPAction:** `ILibrarySoapService/ListAuthors`

```xml
<lib:ListAuthors/>
```

#### UpdateAuthor

**SOAPAction:** `ILibrarySoapService/UpdateAuthor`

```xml
<lib:UpdateAuthor>
   <lib:nAuthorID>1</lib:nAuthorID>
   <lib:cName>Joanne</lib:cName>
   <lib:cSurname>Rowling</lib:cSurname>
</lib:UpdateAuthor>
```

#### DeleteAuthor

**SOAPAction:** `ILibrarySoapService/DeleteAuthor`

```xml
<lib:DeleteAuthor>
   <lib:id>1</lib:id>
</lib:DeleteAuthor>
```

---

### PublishingCompany Requests

#### CreatePublishingCompany

**SOAPAction:** `ILibrarySoapService/CreatePublishingCompany`

```xml
<lib:CreatePublishingCompany>
   <lib:name>Bloomsbury</lib:name>
</lib:CreatePublishingCompany>
```

#### GetPublishingCompanyById

**SOAPAction:** `ILibrarySoapService/GetPublishingCompanyById`

```xml
<lib:GetPublishingCompanyById>
   <lib:id>1</lib:id>
</lib:GetPublishingCompanyById>
```

#### ListPublishingCompanies

**SOAPAction:** `ILibrarySoapService/ListPublishingCompanies`

```xml
<lib:ListPublishingCompanies/>
```

#### UpdatePublishingCompany

**SOAPAction:** `ILibrarySoapService/UpdatePublishingCompany`

```xml
<lib:UpdatePublishingCompany>
   <lib:nPublishingCompanyID>1</lib:nPublishingCompanyID>
   <lib:cName>Bloomsbury Publishing</lib:cName>
</lib:UpdatePublishingCompany>
```

#### DeletePublishingCompany

**SOAPAction:** `ILibrarySoapService/DeletePublishingCompany`

```xml
<lib:DeletePublishingCompany>
   <lib:id>1</lib:id>
</lib:DeletePublishingCompany>
```

---

## Database Migrations

- Create new migration:
`dotnet ef migrations add MigrationName`
--

- Apply migrations to DB:
`dotnet ef database update`
--

- Roll back to baseline:
`dotnet ef database update 20260329133035_InitialCreate`
--

- Roll back all migrations:
`dotnet ef database update 0`
--

- Health check:
`$ dotnet restore && dotnet ef migrations list --project Library.SoapApi.csproj --startup-project Library.SoapApi.csproj`

---

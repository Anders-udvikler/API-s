# SOAP API for Library Management System

SOAP API for managing books, authors, and publishing companies.

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

To use this SOAP API start in the project folder `SOAPAuthorAPI`

- Start in projectfolder **API-s**, then in the terminal write
      `cd SOAPAuthorAPI`

## Quick Start

1. Restore packages:

   ```bash
   dotnet restore
   ```

1. Build:

   ```bash
   dotnet build Library.SoapApi.csproj
   ```

1. Run (HTTPS profile):

   ```bash
   dotnet run --launch-profile https --project Library.SoapApi.csproj
   ```

1. Open SOAP endpoint:

   ```text
   https://localhost:7133/soap
   ```

---

## Postman

1. Import collection file:

   ```text
   postman/LibrarySoapApi.postman_collection_updated.json
   ```

1. Use endpoint:

   ```text
   https://localhost:7133/soap
   ```

See examples of postman requests, import the Postman collection from this [Postman file](postman/Library%20SOAP%20API.postman_collection_updated.json) into postman, and try them out. It is also possible to read more in the full documentation.

## Full Documentation

For complete setup, SOAP examples, faults, migrations, and troubleshooting, see [Documentation.MD](Documentation.MD).

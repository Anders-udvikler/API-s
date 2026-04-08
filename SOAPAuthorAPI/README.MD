# SOAP API for Library Management System

SOAP API for managing books, authors, and publishing companies.

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

## Postman

1. Import collection file:

   ```text
   postman/LibrarySoapApi.postman_collection_updated.json
   ```

1. Use endpoint:

   ```text
   https://localhost:7133/soap
   ```

## Full Documentation

For complete setup, SOAP examples, faults, migrations, and troubleshooting, see [Documentation.MD](Documentation.MD).

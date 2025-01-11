cd into the API project
cd .\TimeTracker.API

Add migration:
dotnet ef migrations add {MigrationName}

Remove last migration
dotnet ef migrations remove

Apply migration
dotnet ef database update